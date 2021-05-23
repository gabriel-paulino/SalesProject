using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Services
{
    public interface IInvoiceService
    {
        Invoice CreateBasedInOrder(Order order);
        Invoice Get(Guid id);
    }
}