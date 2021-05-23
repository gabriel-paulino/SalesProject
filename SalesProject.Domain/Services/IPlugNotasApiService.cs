using SalesProject.Domain.Entities;

namespace SalesProject.Domain.Services
{
    public interface IPlugNotasApiService
    {
        object SendInvoice(Invoice invoice);
    }
}