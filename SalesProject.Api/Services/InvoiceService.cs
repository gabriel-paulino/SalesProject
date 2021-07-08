using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace SalesProject.Api.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public InvoiceService(
            IInvoiceRepository invoiceRepository,
            IOrderRepository orderRepository,
            IUnitOfWork uow)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _uow = uow;
        }

        public Invoice Get(Guid id) =>
            _invoiceRepository.Get(id);

        public object GetInvoiceIdByOrderId(Guid orderId) =>
            _invoiceRepository.GetInvoiceIdByOrderId(orderId);

        public object GetInvoiceIdOfPlugNotasByOrderId(Guid orderId) =>
            _invoiceRepository.GetInvoiceIdOfPlugNotasByOrderId(orderId);

        public Invoice GetByOrderId(Guid orderId) =>
            _invoiceRepository.GetByOrderId(orderId);

        public Invoice CreateBasedInOrder(Order order)
        {
            var invoice = new Invoice(order);
            var invoiceLines = new List<InvoiceLine>();
            var taxLine = new TaxLine();

            foreach (var line in order.OrderLines)
            {
                var currentTaxLine = taxLine.GetDefaultTaxes(line.TotalPrice);
                var invoiceLine = new InvoiceLine(orderLine: line, taxLine: currentTaxLine);

                invoiceLines.Add(invoiceLine);
            }

            foreach (var line in invoiceLines)
                invoice.AddInvoiceLine(line);

            if (!invoice.Valid)
                return invoice;

            order.Bill();

            _orderRepository.Update(order);
            _invoiceRepository.Create(invoice);

            _uow.Commit();

            return invoice;
        }

        public void MarkAsIntegrated(Invoice invoice, string invoiceIdPlugNotas)
        {
            invoice.MarkAsIntegrated(invoiceIdPlugNotas);
            _invoiceRepository.Update(invoice);
            _uow.Commit();
        }

        public List<Invoice> GetAllInvoicesAbleToSend() =>
            _invoiceRepository.GetAllInvoicesAbleToSend();

        public Order GetOrderToCreateInvoice(Guid orderId)
        {
            var order = _orderRepository.GetToCreateInvoice(orderId);

            if (order is null)
                return null;

            if (!order.CanBillThisOrder())
                order.AddNotification("Ops.Apenas pedidos aprovados podem ser faturados.");

            return order;
        }
    }
}