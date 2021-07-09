using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IOrderRepository : IDisposable
    {
        Order Get(Guid id);
        Order GetToCreateInvoice(Guid id);
        ICollection<Order> GetOrdersUsingFilter(OrderFilter filter);
        void Create(Order order);
        void Update(Order order);
        OrderDashboard GetInformationByPeriod(DateTime start, DateTime end);
    }
}