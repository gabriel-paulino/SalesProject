using Microsoft.EntityFrameworkCore;
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
                .Include(o => o.Customer)
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefault(o => o.Id == id);

        public List<Order> GetOrdersUsingFilter(OrderFilter filter)
        {
            IQueryable<Order> query = _context.Orders;
            if (filter.IsFilledCustomerId()) 
                query = query.Where(o => o.CustomerId == filter.CustomerId);
            if (filter.IsFilledOrderStatus()) 
                query = query.Where(o => o.Status == filter.Status);
            if (filter.IsFilledStartDate())
                query = query.Where(o => o.PostingDate >= filter.StartDate);
            if (filter.IsFilledEndDate())
                query = query.Where(o => o.PostingDate <= filter.EndDate);

            return query
                .Include(ol => ol.OrderLines)
                .ToList();
        }

        public void Update(Order order) =>
            _context.Entry<Order>(order).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}