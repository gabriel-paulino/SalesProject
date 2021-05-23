namespace SalesProject.Domain.Dtos
{
    public class AddressApi
    {
        public AddressApi(
            int status,
            string code,
            string state,
            string city,
            string district,
            string address)
        {
            Status = status;
            Code = code;
            State = state;
            City = city;
            District = district;
            Address = address;
        }

        public int Status { get; private set; }
        public string Code { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string District { get; private set; }
        public string Address { get; private set; }
    }

    public class IbgeCode
    {
        public IbgeCode(CidadeInfo cidade_info)
        {
            this.Cidade_info = cidade_info;
        }

        public CidadeInfo Cidade_info { get; private set; }
    }

    public class CidadeInfo
    {
        public CidadeInfo(string codigo_ibge)
        {
            Codigo_ibge = codigo_ibge;
        }

        public string Codigo_ibge { get; private set; }
    }
}