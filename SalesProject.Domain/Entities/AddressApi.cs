namespace SalesProject.Domain.Entities
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
}