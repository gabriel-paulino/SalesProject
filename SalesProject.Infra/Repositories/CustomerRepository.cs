using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;

namespace SalesProject.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public CustomerRepository(DataContext dataContext) =>
            _dataContext = dataContext;
        
        ~CustomerRepository() =>
            Dispose();

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}