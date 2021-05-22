using SalesProject.Domain.Entities;
using SalesProject.Domain.Response;

namespace SalesProject.Domain.Services
{
    public interface IPlugNotasApiService
    {
        PlugNotasResponse SendInvoice(Invoice invoice);
    }
}