using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IOrderRepository : IDisposable
    {
        Order Get(Guid id);
        List<OrderLines> GetLines(Guid id);
        void Create(Order order);
    }
}