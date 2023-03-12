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
            Guid customerId,
            Guid id = default)
        {
            Description = description;
            ZipCode = zipCode;
            Type = type;
            Street = street;
            Neighborhood = neighborhood;
            Number = number;
            City = city;
            State = state;
            CustomerId = customerId;

            if (id != default)
                base.Id = id;

            DoValidations();
        }

        public string Description { get; private set; }
        public string ZipCode { get; private set; }
        public AddressType? Type { get; private set; }
        public string Street { get; private set; }
        public string Neighborhood { get; private set; }
        public int Number { get; private set; }
        public string City { get; private set; }
        public string CodeCity { get; private set; }
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
            Description = description;
            ZipCode = zipCode;
            Type = type;
            Street = street;
            Neighborhood = neighborhood;
            Number = number;
            City = city;
            State = state;

            DoValidations();

            return this;
        }

        public void SetCodeCity(string codeCity) => CodeCity = codeCity;

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
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
                AddNotification("O preenchimento do campo 'UF' é obrigatório.");
            if (Type is null)
                AddNotification("O preenchimento do campo 'Tipo de endereço' é obrigatório.");
            if (Number <= 0)
                AddNotification("O 'Número' informado é inválido.");
        }

        private void ValidateZipCode()
        {
            if (!Validation.Validation.IsValidZipCode(ZipCode))
                AddNotification("O 'Cep' informado é inválido.");
        }
    }
}