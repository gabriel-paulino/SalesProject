using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class InvoiceLine : BaseEntity
    {
        public InvoiceLine() { }

        public InvoiceLine(
            OrderLine orderLine,
            TaxLine taxLine)
        {
            ItemName = orderLine.Product.Name;
            NcmCode = orderLine.Product.NcmCode;
            Quantity = orderLine.Quantity;
            UnitaryPrice = orderLine.UnitaryPrice;
            AdditionalCosts = orderLine.AdditionalCosts;

            OriginIcms = taxLine.OriginIcms;
            CstIcms = taxLine.CstIcms;
            DeterminationMode = taxLine.DeterminationMode;
            ValueBaseCalcIcms = taxLine.ValueBaseCalcIcms;
            AliquotIcms = taxLine.AliquotIcms;
            ValueIcms = taxLine.ValueIcms;
            CstPis = taxLine.CstPis;
            ValueBaseCalcPis = taxLine.ValueBaseCalcPis;
            AliquotPis = taxLine.AliquotPis;
            ValuePis = taxLine.ValuePis;
            CstCofins = taxLine.CstCofins;
            ValueBaseCalcCofins = taxLine.ValueBaseCalcCofins;
            AliquotCofins = taxLine.AliquotCofins;
            ValueCofins = taxLine.ValueCofins;

            DoValidations();
            CalculateTotalLinePrice();
            CalculateTotalTaxes();
        }

        public Guid InvoiceId { get; private set; }
        public string ItemName { get; private set; }
        public string NcmCode { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitaryPrice { get; private set; }
        public decimal TotalPrice { get; private set; }
        public decimal TotalTax { get; private set; }
        public decimal AdditionalCosts { get; private set; }
        public string OriginIcms { get; private set; }
        public string CstIcms { get; private set; }
        public int DeterminationMode { get; private set; }
        public decimal ValueBaseCalcIcms { get; private set; }
        public double AliquotIcms { get; private set; }
        public decimal ValueIcms { get; private set; }
        public string CstPis { get; private set; }
        public decimal ValueBaseCalcPis { get; private set; }
        public double AliquotPis { get; private set; }
        public decimal ValuePis { get; private set; }
        public string CstCofins { get; private set; }
        public decimal ValueBaseCalcCofins { get; private set; }
        public double AliquotCofins { get; private set; }
        public decimal ValueCofins { get; private set; }

        public override void DoValidations()
        {
            if (Quantity <= 0)
                AddNotification("A 'Quantidade' informada é inválida.");
        }

        private void CalculateTotalLinePrice()
        {
            if (!Valid)
                return;

            TotalPrice = Quantity * (UnitaryPrice + AdditionalCosts);
        }

        private void CalculateTotalTaxes() =>
            TotalTax = ValueIcms + ValuePis + ValueCofins;
    }
}
