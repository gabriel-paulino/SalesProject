using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Services
{
    public interface IInvoiceService
    {
        Invoice CreateBasedInOrder(Order order);
        void MarkAsIntegrated(Invoice invoice, string invoiceIdPlugNotas);
        Invoice Get(Guid id);
        Invoice GetByOrderId(Guid orderId);
        object GetInvoiceIdByOrderId(Guid orderId);
        object GetInvoiceIdOfPlugNotasByOrderId(Guid orderId);
    }
}