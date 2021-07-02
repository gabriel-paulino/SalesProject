namespace SalesProject.Domain.Dtos
{
    public class AddressApi
    {
        public AddressApi(
            string cep, 
            string logradouro, 
            string bairro, 
            string localidade, 
            string uf, 
            string ibge)
        {
            Cep = cep;
            Logradouro = logradouro;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
            Ibge = ibge;
        }

        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string Uf { get; private set; }
        public string Ibge { get; private set; }
    }
}