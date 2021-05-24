using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IInvoiceRepository : IDisposable
    {
        void Create(Invoice invoice);
        Invoice Get(Guid id);
        Invoice GetByOrderId(Guid orderId);
        void Update(Invoice invoice);
    }
}