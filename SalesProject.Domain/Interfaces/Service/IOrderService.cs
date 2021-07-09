using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IOrderService
    {
        Order Get(Guid id);
        OrderFilter GetFilterToDashBoard(object orderFilterViewModel);
        ICollection<Order> GetOrdersUsingFilter(OrderFilter filter);
        Order Create(object createOrderViewModel, bool isCustomer, string username);
        Order Edit(Guid id, object editOrderViewModel);
        Order Cancel(Guid id);
        Order Approve(Guid id);
        OrderDashboard GetInformationByPeriod(DateTime start, DateTime end);
    }
}