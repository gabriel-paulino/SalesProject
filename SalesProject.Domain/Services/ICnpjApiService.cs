using SalesProject.Domain.Entities;

namespace SalesProject.Domain.Services
{
    public interface ICnpjApiService
    {
        CnpjApi CompleteCustomerApi(string cnpj);
    }
}