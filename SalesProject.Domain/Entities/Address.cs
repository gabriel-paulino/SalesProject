using SalesProject.Domain.Entities.Base;
using System;

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
            string country)
        {
            ZipCode = zipCode;
            Street = street;
            Neighborhood = neighborhood;
            Number = number;
            City = city;
            State = state;
            Country = country;
        }

        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string Neighborhood { get; private set; }
        public int Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public Guid ClientId { get; private set; }

        public override void DoBusinesRulesValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}
