namespace SalesProject.Domain.Dtos
{
    public class CnpjApi
    {
        public CnpjApi(
            string status, 
            string cnpj, 
            string nome, 
            string telefone, 
            string abertura, 
            string message)
        {
            Status = status;
            Cnpj = cnpj;
            Nome = nome;
            Telefone = telefone;
            Abertura = abertura;
            Message = message;
        }

        public string Status { get; private set; }
        public string Cnpj { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Abertura { get; private set; }
        public string Message { get; private set; }
    }
}