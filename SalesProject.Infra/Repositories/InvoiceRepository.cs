using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;

namespace SalesProject.Infra.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public InvoiceRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        ~InvoiceRepository() =>
            Dispose();

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}