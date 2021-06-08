using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IInvoiceRepository : IDisposable
    {
        void Create(Invoice invoice);
        Invoice Get(Guid id);
        Invoice GetByOrderId(Guid orderId);
        object GetInvoiceIdByOrderId(Guid orderId);
        object GetInvoiceIdOfPlugNotasByOrderId(Guid orderId);
        List<Invoice> GetAllInvoicesAbleToSend();
        void Update(Invoice invoice);
    }
}