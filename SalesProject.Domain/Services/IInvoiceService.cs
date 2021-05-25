using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Services
{
    public interface IInvoiceService
    {
        Invoice CreateBasedInOrder(Order order);
        void MarkAsIntegrated(Invoice invoice);
        Invoice Get(Guid id);
        Invoice GetByOrderId(Guid orderId);
    }
}