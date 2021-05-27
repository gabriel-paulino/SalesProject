using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Linq;

namespace SalesProject.Infra.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _context;
        private bool _disposed = false;

        public InvoiceRepository(DataContext dataContext) =>
            _context = dataContext;

        ~InvoiceRepository() =>
            Dispose();

        public object GetInvoiceIdByOrderId(Guid orderId)
        {
            return
            (from invoice in _context.Invoices
             where invoice.OrderId == orderId
             select new
             {
                 invoice.Id
             }).FirstOrDefault();
        }

        public object GetInvoiceIdOfPlugNotasByOrderId(Guid orderId)
        {
            return
            (from invoice in _context.Invoices
             where invoice.OrderId == orderId
             select new
             {
                 invoice.IdPlugNotasIntegration
             }).FirstOrDefault();
        }

        public Invoice Get(Guid id) =>
            _context.Invoices
                .Include(i => i.InvoiceLines)
                .Include(i => i.Order)
                .ThenInclude(o => o.Customer)
                .ThenInclude(c => c.Adresses)
                .FirstOrDefault(i => i.Id == id);

        public Invoice GetByOrderId(Guid orderId) =>
            _context.Invoices
                .Include(i => i.InvoiceLines)
                .Include(i => i.Order)
                .ThenInclude(o => o.Customer)
                .ThenInclude(c => c.Adresses)
                .FirstOrDefault(i => i.OrderId == orderId);

        public void Create(Invoice invoice) =>
            _context.Invoices.Add(invoice);

        public void Update(Invoice invoice) =>
            _context.Entry<Invoice>(invoice).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}