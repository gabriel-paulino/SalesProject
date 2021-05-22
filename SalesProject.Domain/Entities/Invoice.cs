using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public Invoice() { }

        public Invoice(Order order)
        {
            Order = order;
            ReleaseDate = DateTime.Today.Date;
            OriginOperation = "Vendas";
            _invoiceLines = new List<InvoiceLine>();

            DoValidations();
        }

        private IList<InvoiceLine> _invoiceLines;

        public Order Order { get; private set; }
        public Guid? OrderId { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public string OriginOperation { get; private set; }
        public decimal BaseCalcIcms { get; private set; }
        public decimal TotalIcms { get; private set; }
        public decimal TotalProducts { get; private set; }
        public decimal TotalInvoice { get; private set; }

        public IReadOnlyCollection<InvoiceLine> InvoiceLines { get => _invoiceLines.ToArray(); }

        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            _invoiceLines.Add(invoiceLine);
            UpdateInvoiceValues();
        }

        public override void DoValidations()
        {
        }

        private void UpdateInvoiceValues()
        {
            decimal total = InvoiceLines.Sum(l => l.TotalPrice);

            TotalInvoice = total;
            TotalProducts = total;
            BaseCalcIcms = total;

            TotalIcms = InvoiceLines.Sum(l => l.ValueIcms);
        }
    }
}