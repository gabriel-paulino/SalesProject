using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;

namespace SalesProject.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public ContactRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        ~ContactRepository() =>
            Dispose();

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}