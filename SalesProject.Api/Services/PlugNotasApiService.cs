using Microsoft.Extensions.Configuration;
using RestSharp;
using SalesProject.Domain.Dtos;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace SalesProject.Api.Services
{
    public class PlugNotasApiService : IPlugNotasApiService
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public PlugNotasApiService(
            IInvoiceService invoiceService,
            IConfiguration configuration)
        {
            _invoiceService = invoiceService;
            _configuration = configuration;
            _url = "https://api.sandbox.plugnotas.com.br/";
        }

        public object SendInvoice(Invoice invoice)
        {
            var invoicePlugNotas = Initialize(invoice);
            var options = GetOptionsForSerializationJson();

            string jsonInvoice = JsonSerializer.Serialize(invoicePlugNotas, options);

            return Send($"[{jsonInvoice}]");
        }

        public string SendAllInvoices(List<Invoice> invoices)
        {
            var invoicesPlugNotas = new List<PlugNotasApi>();

            foreach (var invoice in invoices)
            {
                var invoicePlugNotas = Initialize(invoice);
                invoicesPlugNotas.Add(invoicePlugNotas);
            }

            var options = GetOptionsForSerializationJson();

            string jsonInvoices = JsonSerializer.Serialize(invoicesPlugNotas, options);

            var response = Send(jsonInvoices);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var plugNotasResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PlugNotasResponse>(response.Content);

                foreach (var invoice in invoices)
                {
                    foreach (var document in plugNotasResponse.Documents)
                    {
                        if (document.IdIntegracao.ToUpper() != invoice.Id.ToString().ToUpper())
                            continue;

                        _invoiceService.MarkAsIntegrated(invoice, document.Id);
                    }
                }
                return string.Empty;
            }

            return response.Content;
        }

        public object ConsultSefaz(string invoiceIdPlugNotas)
        {
            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest($"nfe/{invoiceIdPlugNotas}/resumo", Method.GET);
            request.AddHeader("x-api-key", _configuration["PlugNotasApiKey"]);
            request.OnBeforeDeserialization = response => { response.ContentType = "application/json"; };

            return client.Execute(request);
        }

        public string DownloadInvoicePdf(string invoiceIdPlugNotas)
        {
            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest($"nfe/{invoiceIdPlugNotas}/pdf", Method.GET);
            request.AddHeader("x-api-key", _configuration["PlugNotasApiKey"]);
            request.OnBeforeDeserialization = response => { response.ContentType = "application/pdf"; };

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                byte[] document = client.DownloadData(request);

                string path = $@"C:/nota-fiscal/pdf/{invoiceIdPlugNotas}.pdf";

                FileInfo file = new FileInfo(path);
                file.Directory.Create();
                File.WriteAllBytes(file.FullName, document);

                return string.Empty;
            }
            return response.Content;
        }

        public string DownloadInvoiceXml(string invoiceIdPlugNotas)
        {
            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest($"nfe/{invoiceIdPlugNotas}/cce/xml", Method.GET);
            request.AddHeader("x-api-key", _configuration["PlugNotasApiKey"]);
            request.OnBeforeDeserialization = response => { response.ContentType = "application/xml"; };

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                byte[] document = client.DownloadData(request);

                string path = $@"C:/nota-fiscal/xml/{invoiceIdPlugNotas}.xml";

                FileInfo file = new FileInfo(path);
                file.Directory.Create();
                File.WriteAllBytes(file.FullName, document);

                return string.Empty;
            }
            return response.Content;
        }

        private PlugNotasApi Initialize(Invoice invoice)
        {
            var plugNotas = new PlugNotasApi();

            var address = invoice.Order.Customer.Adresses.Where(a => a.Type == AddressType.Billing).FirstOrDefault();

            plugNotas.IdIntegracao = invoice.Id.ToString();
            plugNotas.Presencial = true;
            plugNotas.ConsumidorFinal = true;
            plugNotas.Natureza = invoice.OriginOperation;

            var issuer = new Emitente();
            issuer.CpfCnpj = _configuration["IssuerCnpj"];

            plugNotas.Emitente = issuer;

            var receiver = new Destinatario();

            receiver.CpfCnpj = invoice.Order.Customer.Cnpj;
            receiver.RazaoSocial = invoice.Order.Customer.CompanyName;
            receiver.Email = invoice.Order.Customer.Email;

            var receiverAddress = new Endereco();

            receiverAddress.Logradouro = address.Street;
            receiverAddress.Numero = address.Number.ToString();
            receiverAddress.Bairro = address.Neighborhood;
            receiverAddress.CodigoCidade = address.CodeCity;
            receiverAddress.DescricaoCidade = address.City;
            receiverAddress.Estado = address.State;
            receiverAddress.Cep = address.ZipCode;

            receiver.Endereco = receiverAddress;

            plugNotas.Destinatario = receiver;

            var itemsPlugNotas = new List<Iten>();

            foreach (var item in invoice.InvoiceLines)
            {
                var itemPlugNotas = new Iten();

                itemPlugNotas.Codigo = item.ItemName;
                itemPlugNotas.Descricao = item.ItemName;
                itemPlugNotas.Ncm = item.NcmCode;
                itemPlugNotas.Cfop = "5101";

                var unitaryValue = new ValorUnitario();
                unitaryValue.Comercial = (double)item.UnitaryPrice;
                unitaryValue.Tributavel = (double)item.UnitaryPrice;
                itemPlugNotas.ValorUnitario = unitaryValue;

                var quantity = new Quantidade();
                quantity.Comercial = item.Quantity;
                quantity.Tributavel = item.Quantity;
                itemPlugNotas.Quantidade = quantity;

                var taxes = new Tributos();

                var icms = new Icms();
                icms.Origem = item.OriginIcms;
                icms.Cst = item.CstIcms;
                var baseCalcIcms = new BaseCalculo();
                baseCalcIcms.ModalidadeDeterminacao = item.DeterminationMode;
                baseCalcIcms.Valor = (double)item.ValueBaseCalcIcms;
                icms.BaseCalculo = baseCalcIcms;
                icms.Aliquota = item.AliquotIcms;
                icms.Valor = (double)item.ValueIcms;
                taxes.Icms = icms;

                var pis = new Pis();
                pis.Cst = item.CstPis;
                var baseCalcPis = new BaseCalculo();
                baseCalcPis.Valor = (double)item.ValueBaseCalcPis;
                pis.BaseCalculo = baseCalcPis;
                pis.Aliquota = item.AliquotPis;
                pis.Valor = (double)item.ValuePis;
                taxes.Pis = pis;

                var cofins = new Cofins();
                cofins.Cst = item.CstCofins;
                var baseCalcCofins = new BaseCalculo();
                baseCalcCofins.Valor = (double)item.ValueBaseCalcCofins;
                cofins.BaseCalculo = baseCalcCofins;
                cofins.Aliquota = item.AliquotCofins;
                cofins.Valor = (double)item.ValueCofins;
                taxes.Cofins = cofins;

                itemPlugNotas.Tributos = taxes;

                itemsPlugNotas.Add(itemPlugNotas);
            }

            plugNotas.Itens = itemsPlugNotas;

            var payments = new List<Pagamento>();

            var payment = new Pagamento();
            payment.AVista = true;
            payment.Meio = "18";
            payments.Add(payment);

            plugNotas.Pagamentos = payments;

            return plugNotas;
        }

        private JsonSerializerOptions GetOptionsForSerializationJson() =>
            new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        private IRestResponse Send(string json)
        {
            var client = new RestClient($"{_url}nfe");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-api-key", _configuration["PlugNotasApiKey"]);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(json);

            return client.Execute(request);
        }
    }
}
