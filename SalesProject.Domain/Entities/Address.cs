using SalesProject.Domain.Entities.Base;

namespace SalesProject.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address(
            string zipCode, 
            string street, 
            string neighborhood, 
            int number, 
            string city, 
            string state, 
            string country, 
            int idClient)
        {
            ZipCode = zipCode;
            Street = street;
            Neighborhood = neighborhood;
            Number = number;
            City = city;
            State = state;
            Country = country;
            IdClient = idClient;
        }

        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string Neighborhood { get; private set; }
        public int Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public int IdClient { get; private set; }

        public override void DoBusinesRulesValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}
