using Microsoft.Extensions.Configuration;
using RestSharp;
using SalesProject.Domain.Dtos;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace SalesProject.Api.Services
{
    public class PlugNotasApiService : IPlugNotasApiService
    {
        private readonly IConfiguration _configuration;

        public PlugNotasApiService(IConfiguration configuration) =>
            _configuration = configuration;

        public object SendInvoice(Invoice invoice)
        {
            var plugNotasApi = Initialize(invoice);

            var config = new JsonSerializerOptions();

            config.IgnoreNullValues = true;
            config.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            string jsonInvoice = JsonSerializer.Serialize(plugNotasApi, options: config);

            string url = "https://api.sandbox.plugnotas.com.br/nfe";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-api-key", _configuration["PlugNotasApiKey"]);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody($"[{jsonInvoice}]");
            IRestResponse response = client.Execute(request);

            return response;
        }

        private PlugNotasApi Initialize(Invoice invoice)
        {
            var plugNotas = new PlugNotasApi();
            var items = new List<Iten>();

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

            var itensPlugNotas = new List<Iten>();

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

                itensPlugNotas.Add(itemPlugNotas);
            }

            plugNotas.Itens = itensPlugNotas;

            var payments = new List<Pagamento>();

            var payment = new Pagamento();
            payment.AVista = true;
            payment.Meio = "18";
            payments.Add(payment);

            plugNotas.Pagamentos = payments;

            return plugNotas;
        }
    }
}
