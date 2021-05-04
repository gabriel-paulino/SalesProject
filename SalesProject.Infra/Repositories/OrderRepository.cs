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
            if(filter.IsFilledCustomerId() && filter.IsFilledOrderStatus())
                return _context.Orders
                    .Include(ol => ol.OrderLines)
                    .Where(o => o.CustomerId == filter.CustomerId &&
                           o.Status == filter.Status &&
                           o.PostingDate >= (filter.StartDate ?? DateTime.MinValue) &&
                           o.PostingDate <= (filter.EndDate ?? DateTime.MaxValue) && true)
                    .ToList();

            else if (filter.IsFilledCustomerId() && !filter.IsFilledOrderStatus())
                return _context.Orders
                    .Include(ol => ol.OrderLines)
                    .Where(o => o.CustomerId == filter.CustomerId &&
                           o.PostingDate >= (filter.StartDate ?? DateTime.MinValue) &&
                           o.PostingDate <= (filter.EndDate ?? DateTime.MaxValue) && true)
                    .ToList();

            else if (!filter.IsFilledCustomerId() && filter.IsFilledOrderStatus())
                return _context.Orders
                    .Include(ol => ol.OrderLines)
                    .Where(o => o.Status == filter.Status &&
                           o.PostingDate >= (filter.StartDate ?? DateTime.MinValue) &&
                           o.PostingDate <= (filter.EndDate ?? DateTime.MaxValue) && true)
                    .ToList();

            return _context.Orders
                    .Include(ol => ol.OrderLines)
                    .Where(o => o.PostingDate >= (filter.StartDate ?? DateTime.MinValue) &&
                           o.PostingDate <= (filter.EndDate ?? DateTime.MaxValue))
                    .ToList();
        }

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}