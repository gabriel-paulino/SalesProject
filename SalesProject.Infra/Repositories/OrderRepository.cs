using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Create(Order order) =>
            _dataContext.Orders.Add(order);

        public Order Get(Guid id) =>
            _dataContext.Orders.Find(id);

        public List<OrderLines> GetLines (Guid id) =>
            _dataContext.Orders.
            SelectMany(line => line.OrderLines).
            Where(line => line.OrderId == id).
            ToList();

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}