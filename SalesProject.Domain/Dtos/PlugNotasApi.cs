using System.Collections.Generic;

namespace SalesProject.Domain.Dtos
{
    public class Emitente
    {
        public string CpfCnpj { get; set; }
    }

    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string CodigoCidade { get; set; }
        public string DescricaoCidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
    }

    public class Destinatario
    {
        public string CpfCnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
    }

    public class ValorUnitario
    {
        public double Comercial { get; set; }
        public double Tributavel { get; set; }
    }

    public class Quantidade
    {
        public int Comercial { get; set; }
        public int Tributavel { get; set; }
    }

    public class BaseCalculo
    {
        public int ModalidadeDeterminacao { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
    }

    public class Icms
    {
        public string Origem { get; set; }
        public string Cst { get; set; }
        public BaseCalculo BaseCalculo { get; set; }
        public double Aliquota { get; set; }
        public double Valor { get; set; }
    }

    public class Pis
    {
        public string Cst { get; set; }
        public BaseCalculo BaseCalculo { get; set; }
        public double Aliquota { get; set; }
        public double Valor { get; set; }
    }

    public class Cofins
    {
        public string Cst { get; set; }
        public BaseCalculo BaseCalculo { get; set; }
        public double Aliquota { get; set; }
        public double Valor { get; set; }
    }

    public class Tributos
    {
        public Icms Icms { get; set; }
        public Pis Pis { get; set; }
        public Cofins Cofins { get; set; }
    }

    public class Iten
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Ncm { get; set; }
        public string Cfop { get; set; }
        public ValorUnitario ValorUnitario { get; set; }
        public Quantidade Quantidade { get; set; }
        public Tributos Tributos { get; set; }
    }

    public class Pagamento
    {
        public bool AVista { get; set; }
        public string Meio { get; set; }
        public double Valor { get; set; }
    }

    public class PlugNotasApi
    {
        public string IdIntegracao { get; set; }
        public bool Presencial { get; set; }
        public bool ConsumidorFinal { get; set; }
        public string Natureza { get; set; }
        public Emitente Emitente { get; set; }
        public Destinatario Destinatario { get; set; }
        public List<Iten> Itens { get; set; }
        public List<Pagamento> Pagamentos { get; set; }
    }

    public class Document
    {
        public Document(
            string idIntegracao, 
            string emitente, 
            string id)
        {
            IdIntegracao = idIntegracao;
            Emitente = emitente;
            Id = id;
        }

        public string IdIntegracao { get; set; }
        public string Emitente { get; set; }
        public string Id { get; set; }
    }

    public class PlugNotasResponse
    {
        public PlugNotasResponse(
            List<Document> documents, 
            string message, 
            string protocol)
        {
            Documents = documents;
            Message = message;
            Protocol = protocol;
        }

        public List<Document> Documents { get; set; }
        public string Message { get; set; }
        public string Protocol { get; set; }
    }
}