using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
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

        public Order GetToCreateInvoice(Guid id) =>
            _context.Orders
                .Include(o => o.Customer)
                .ThenInclude(c => c.Adresses)
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefault(o => o.Id == id);

        public ICollection<Order> GetOrdersUsingFilter(OrderFilter filter)
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
                .Include(o => o.OrderLines)
                .ToList();
        }

        public void Update(Order order) =>
            _context.Entry<Order>(order).State = EntityState.Modified;

        public OrderDashboard GetInformationByPeriod(DateTime start, DateTime end)
        {
            IQueryable<Order> query =
                _context
                .Orders
                .Where(o => o.PostingDate >= start && o.PostingDate <= end);

            return query.Any()
                ? new OrderDashboard(
                    start: start,
                    end: end,
                    openOrders: query.Where(o => o.Status == OrderStatus.Open).Count(),
                    approvedOrders: query.Where(o => o.Status == OrderStatus.Approved).Count(),
                    canceledOrders: query.Where(o => o.Status == OrderStatus.Canceled).Count(),
                    billedOrders: query.Where(o => o.Status == OrderStatus.Billed).Count(),
                    biggestOrder: query.Max(o => o.TotalOrder),
                    lowestOrder: query.Min(o => o.TotalOrder),
                    averageOrders: query.Average(o => o.TotalOrder),
                    totalSales: query.Where(o => o.Status == OrderStatus.Billed).Sum(o => o.TotalOrder)
                    )
                : null;
        }

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}