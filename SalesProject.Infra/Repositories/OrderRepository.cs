using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Linq;

namespace SalesProject.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private bool _disposed = false;

        public OrderRepository(DataContext context) =>
            _context = context;

        ~OrderRepository() =>
            Dispose();

        public void Create(Order order) =>
            _context.Orders.Add(order);

        public Order Get(Guid id) =>
            _context.Orders
                .AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefault(o => o.Id == id);

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}