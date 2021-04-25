using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IOrderRepository : IDisposable
    {
        Order Get(Guid id);
        void Create(Order order);
    }
}