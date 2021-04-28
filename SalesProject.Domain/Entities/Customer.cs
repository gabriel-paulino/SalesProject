﻿using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.ClientSince = DateTime.Today.Date;
            _adresses = new List<Address>();
            _contacts = new List<Contact>();
            _products = new List<Product>();
        }

        public Customer(
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
            this.ClientSince = DateTime.Today.Date;
            this.MunicipalRegistration = municipalRegistration;
            _adresses = new List<Address>();
            _contacts = new List<Contact>();
            _products = new List<Product>();

            DoValidations();
        }

        private IList<Address> _adresses;
        private IList<Contact> _contacts;
        private IList<Product> _products;

        public string Cnpj { get; private set; }
        public string CompanyName { get; private set; }
        public string StateRegistration { get; private set; }
        public DateTime? Opening { get; private set; }
        public string Phone { get; private set; }
        public DateTime ClientSince { get; private set; }
        public string MunicipalRegistration { get; private set; }
        public IReadOnlyCollection<Address> Adresses { get => _adresses.ToArray(); }
        public IReadOnlyCollection<Contact> Contacts { get => _contacts.ToArray(); }
        public IReadOnlyCollection<Product> Products { get => _products.ToArray(); }
        public User User { get; private set; }

        public Customer Edit(
            string phone,
            string municipalRegistration,
            string stateRegistration)
        {
            this.StateRegistration = stateRegistration;
            this.Phone = phone;
            this.MunicipalRegistration = municipalRegistration;

            DoValidations();

            return this;
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
            if (!Validation.Validation.IsValidCnpj(Cnpj))
                AddNotification("O 'Cnpj' informado é inválido.");
        }
    }
}