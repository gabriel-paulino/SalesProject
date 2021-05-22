using SalesProject.Domain.Entities;

namespace SalesProject.Domain.Services
{
    public interface IInvoiceService
    {
        Invoice CreateBasedInOrder(Order order);
    }
}