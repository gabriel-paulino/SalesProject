using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product() { }

        public Product(
            string name,
            string ncmCode,
            decimal combinedPrice,
            decimal additionalCosts,
            double combinedQuantity,
            string details,
            Customer customer)
        {
            this.Name = name;
            this.NcmCode = ncmCode;
            this.CombinedPrice = combinedPrice;
            this.AdditionalCosts = additionalCosts;
            this.CombinedQuantity = combinedQuantity;
            this.Details = details;
            this.Customer = customer;

            DoValidations();
        }

        public string Name { get; private set; }
        public string NcmCode { get; private set; }
        public decimal CombinedPrice { get; private set; }
        public decimal AdditionalCosts { get; private set; }
        public double CombinedQuantity { get; private set; }
        public string Details { get; private set; }
        public Guid? CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
        }

        private void ValidateFillingMandatoryFields()
        {
            if (string.IsNullOrEmpty(Name))
                AddNotification("O preenchimento do campo 'Nome do produto' é obrigatório.");
            if (CombinedPrice <= 0)
                AddNotification("Valor do campo 'Preço combinado' está inválido.");
            if (CombinedQuantity <= 0)
                AddNotification("A 'Previsão mínima mensal' informada é inválida.");
            if (CombinedQuantity < 0)
                AddNotification("O 'Custo adicional' informado é inválido.");
            if(Customer == null)
                AddNotification("O preenchimento do campo 'Cliente' é obrigatório.");
        }
    }
}