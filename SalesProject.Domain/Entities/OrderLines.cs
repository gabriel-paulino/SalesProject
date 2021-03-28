using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class OrderLines : BaseEntity
    {
        public OrderLines() { }

        public OrderLines(
            double quantity,
            decimal unitaryPrice,
            double percentageUnitaryDiscont,
            decimal additionalCosts,
            Product product,
            TaxLine taxLine)
        {
            this.Quantity = quantity;
            this.UnitaryPrice = unitaryPrice;
            this.PercentageUnitaryDiscont = percentageUnitaryDiscont;
            this.ValueUnitaryDiscont = percentageUnitaryDiscont > 0 ? (decimal)percentageUnitaryDiscont * unitaryPrice : 0;
            this.AdditionalCosts = additionalCosts;

            //Todo: Ver quais atributos fazem sentido serem atribuidos no construtor
            this.Cst = taxLine.Cst;
            this.Cfop = taxLine.Cfop;
            this.BaseCalcIcms = taxLine.BaseCalcIcms;
            this.IcmsValue = taxLine.IcmsValue;
            this.IpiValue = taxLine.IpiValue;
            this.IcmsAliquot = taxLine.IcmsAliquot;
            this.IpiAliquot = taxLine.IpiAliquot;

            this.Product = product;
            this.TaxLine = taxLine;

            DoValidations();
            CalculateTotalLinePrice();
        }

        public OrderLines(
            Product product,
            TaxLine taxLine)
        {
            this.Quantity = product.CombinedQuantity;
            this.UnitaryPrice = product.CombinedPrice;
            this.AdditionalCosts = product.AdditionalCosts;

            //Todo: Ver quais atributos fazem sentido serem atribuidos no construtor
            this.Cst = taxLine.Cst;
            this.Cfop = taxLine.Cfop;
            this.BaseCalcIcms = taxLine.BaseCalcIcms;
            this.IcmsValue = taxLine.IcmsValue;
            this.IpiValue = taxLine.IpiValue;
            this.IcmsAliquot = taxLine.IcmsAliquot;
            this.IpiAliquot = taxLine.IpiAliquot;

            this.Product = product;
            this.TaxLine = taxLine;

            DoValidations();
            CalculateTotalLinePrice();
        }

        public Guid OrderId { get; private set; }
        public double Quantity { get; private set; }
        public decimal UnitaryPrice { get; private set; }
        public decimal TotalPrice { get; private set; }
        public double PercentageUnitaryDiscont { get; private set; }
        public decimal ValueUnitaryDiscont { get; private set; }
        public decimal AdditionalCosts { get; private set; }

        public string Cst { get; private set; }
        public string Cfop { get; private set; }
        public decimal BaseCalcIcms { get; private set; }
        public decimal IcmsValue { get; private set; }
        public decimal IpiValue { get; private set; }
        public double IcmsAliquot { get; private set; }
        public double IpiAliquot { get; private set; }

        public Product Product { get; private set; }
        public TaxLine TaxLine { get; private set; }

        public override void DoValidations()
        {
            if (Quantity <= 0)
                AddNotification("O 'Quantidade' informada é inválida.");
            if (PercentageUnitaryDiscont > 1 && PercentageUnitaryDiscont < 0)
                AddNotification("O 'Desconto' informado é inválido.");
        }

        private void CalculateTotalLinePrice()
        {
            if (!Valid)
                return;

            TotalPrice = (decimal)Quantity * (UnitaryPrice - ValueUnitaryDiscont);
            TotalPrice = AdditionalCosts > 0 ? TotalPrice + AdditionalCosts : TotalPrice;
            CalculateTax();
        }

        private void CalculateTax()
        {
            //Todo
        }
    }
}