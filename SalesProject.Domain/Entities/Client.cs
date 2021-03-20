using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class Client : BaseEntity
    {
        private IList<Address> _adresses;
        private IList<Contact> _contacts;

        public Client() { }

        public Client(
            string cnpj, 
            string companyName, 
            DateTime opening, 
            string telNumber, 
            string municipalRegistration, 
            string stateRegistration)
        {      
            this.Cnpj = cnpj;
            this.CompanyName = companyName;
            this.Opening = opening;
            this.TelNumber = telNumber;
            this.ClientSince = DateTime.Today;
            this.MunicipalRegistration = municipalRegistration;
            this.StateRegistration = stateRegistration;
            _adresses = new List<Address>();
            _contacts = new List<Contact>();

            DoBusinesRulesValidations();
        }
       
        public string Cnpj { get; private set; }
        public string CompanyName { get; private set; }
        public DateTime Opening { get; private set; }
        public string TelNumber { get; private set; }
        public DateTime ClientSince { get; private set; }
        public string MunicipalRegistration { get; private set; }
        public string StateRegistration { get; private set; }
        public IReadOnlyCollection<Address> Adresses { get { return _adresses.ToArray(); } }
        public IReadOnlyCollection<Contact> Contacts { get { return _contacts.ToArray(); } }
 
        public void AddAddress(Address address)
        {
            _adresses.Add(address);
        }

        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }

        public override void DoBusinesRulesValidations()
        {
            throw new NotImplementedException();
        }
    }
}