using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IOrderRepository : IDisposable
    {
        Order Get(Guid id);
        List<Order> GetOrdersUsingFilter(OrderFilter filter);
        void Create(Order order);
    }
}