using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;

namespace SalesProject.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public ProductRepository(DataContext dataContext) =>       
            _dataContext = dataContext;

        ~ProductRepository() =>
            Dispose();

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}