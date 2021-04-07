using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;

namespace SalesProject.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public OrderRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        ~OrderRepository() =>
            Dispose();

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}