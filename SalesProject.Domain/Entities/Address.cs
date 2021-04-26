using SalesProject.Domain.Entities.Base;
using SalesProject.Domain.Enums;
using System;

namespace SalesProject.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address(
            string description,
            string zipCode,
            AddressType? type,
            string street,
            string neighborhood,
            int number,
            string city,
            string state,
            Guid customerId)
        {
            this.Description = description;
            this.ZipCode = zipCode;
            this.Type = type;
            this.Street = street;
            this.Neighborhood = neighborhood;
            this.Number = number;
            this.City = city;
            this.State = state;
            this.CustomerId = customerId;

            DoValidations();
        }

        public string Description { get; private set; }
        public string ZipCode { get; private set; }
        public AddressType? Type { get; private set; }
        public string Street { get; private set; }
        public string Neighborhood { get; private set; }
        public int Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid CustomerId { get; private set; }

        public Address Edit(
            string description,
            string zipCode,
            AddressType? type,
            string street,
            string neighborhood,
            int number,
            string city,
            string state)
        {
            this.Description = description;
            this.ZipCode = zipCode;
            this.Type = type;
            this.Street = street;
            this.Neighborhood = neighborhood;
            this.Number = number;
            this.City = city;
            this.State = state;

            DoValidations();

            return this;
        }

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
            ValidateAddressType();
            ValidateZipCode();
        }

        private void ValidateFillingMandatoryFields()
        {
            if (string.IsNullOrEmpty(Description))
                AddNotification("O preenchimento do campo 'Descrição' é obrigatório.");
            if (string.IsNullOrEmpty(ZipCode))
                AddNotification("O preenchimento do campo 'Cep' é obrigatório.");
            if (string.IsNullOrEmpty(Street))
                AddNotification("O preenchimento do campo 'Logradouro' é obrigatório.");
            if (string.IsNullOrEmpty(Neighborhood))
                AddNotification("O preenchimento do campo 'Bairro' é obrigatório.");
            if (string.IsNullOrEmpty(City))
                AddNotification("O preenchimento do campo 'Cidade' é obrigatório.");
            if (string.IsNullOrEmpty(State))
                AddNotification("O preenchimento do campo 'Uf' é obrigatório.");
            if (Number <= 0)
                AddNotification("O 'Número' informado é inválido.");
        }

        private void ValidateAddressType()
        {
            if (Type != AddressType.Billing &&
                Type != AddressType.Delivery &&
                Type != AddressType.Other)
                AddNotification("O 'Tipo de endereço' informado é inválido.");
        }

        private void ValidateZipCode()
        {
            if (!Validation.Validation.IsValidZipCode(ZipCode))
                AddNotification("O 'Cep' informado é inválido.");
        }  
    }
}