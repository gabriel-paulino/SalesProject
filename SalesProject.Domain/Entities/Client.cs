using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class Client : BaseEntity
    {
        public Client() { }

        public Client(
            string cnpj,
            string companyName,
            DateTime? opening,
            string phone,
            string municipalRegistration,
            string stateRegistration)
        {
            this.Cnpj = cnpj;
            this.CompanyName = companyName;
            this.StateRegistration = stateRegistration;
            this.Opening = opening;
            this.Phone = phone;
            this.ClientSince = DateTime.Today;
            this.MunicipalRegistration = municipalRegistration;
            _adresses = new List<Address>();
            _contacts = new List<Contact>();

            DoValidations();
        }

        private IList<Address> _adresses;
        private IList<Contact> _contacts;

        public string Cnpj { get; private set; }
        public string CompanyName { get; private set; }
        public string StateRegistration { get; private set; }
        public DateTime? Opening { get; private set; }
        public string Phone { get; private set; }
        public DateTime ClientSince { get; private set; }
        public string MunicipalRegistration { get; private set; }

        public IReadOnlyCollection<Address> Adresses { get => _adresses.ToArray(); }
        public IReadOnlyCollection<Contact> Contacts { get => _contacts.ToArray(); }

        public void AddAddress(Address address)
        {
            _adresses.Add(address);
        }

        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
            ValidateCnpj();
        }

        private void ValidateFillingMandatoryFields()
        {
            if (string.IsNullOrEmpty(Cnpj))
                AddNotification("O preenchimento do campo 'Cnpj' é obrigatório.");
            if (string.IsNullOrEmpty(CompanyName))
                AddNotification("O preenchimento do campo 'Nome da empresa' é obrigatório.");
            if (string.IsNullOrEmpty(StateRegistration))
                AddNotification("O preenchimento do campo 'Inscrição estadual' é obrigatório.");
        }

        private void ValidateCnpj()
        {
            if (!Validation.CnpjIsValid(Cnpj))
                AddNotification("O 'Cnpj' informado é inválido.");
        }
    }
}