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
            int combinedQuantity,
            string details,
            Guid? customerId)
        {
            this.Name = name;
            this.NcmCode = ncmCode;
            this.CombinedPrice = combinedPrice;
            this.AdditionalCosts = additionalCosts;
            this.CombinedQuantity = combinedQuantity;
            this.Details = details;
            this.CustomerId = customerId;

            DoValidations();
        }

        public string Name { get; private set; }
        public string NcmCode { get; private set; }
        public decimal CombinedPrice { get; private set; }
        public decimal AdditionalCosts { get; private set; }
        public int CombinedQuantity { get; private set; }
        public string Details { get; private set; }
        public Guid? CustomerId { get; private set; }

        public Product Edit (
            string name,
            string ncmCode,
            decimal combinedPrice,
            decimal additionalCosts,
            int combinedQuantity,
            string details)
        {
            this.Name = name;
            this.NcmCode = ncmCode;
            this.CombinedPrice = combinedPrice;
            this.AdditionalCosts = additionalCosts;
            this.CombinedQuantity = combinedQuantity;
            this.Details = details;

            DoValidations();

            return this;
        }

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
            ValidateNumericFields();
            ValidateNcmCode();
        }

        private void ValidateFillingMandatoryFields()
        {
            if (string.IsNullOrEmpty(Name))
                AddNotification("O preenchimento do campo 'Nome do produto' é obrigatório.");
            if (string.IsNullOrEmpty(NcmCode))
                AddNotification("O preenchimento do campo 'Código Ncm' é obrigatório.");
            
        }

        private void ValidateNumericFields()
        {
            if (CombinedPrice <= 0)
                AddNotification("Valor do campo 'Preço combinado' está inválido.");
            if (CombinedQuantity <= 0)
                AddNotification("A 'Previsão mínima mensal' informada é inválida.");
            if (AdditionalCosts < 0)
                AddNotification("O 'Custo adicional' informado é inválido.");
        }

        private void ValidateNcmCode()
        {
            if(!(NcmCode.Length == 2 || NcmCode.Length == 8))
                AddNotification("O 'Código Ncm' deve possuir 2 ou 8 caractéres.");
            if(!(int.TryParse(NcmCode, out int result)))
                AddNotification("O 'Código Ncm' informado é inválido.");
        }
    }
}