using SalesProject.Domain.Dtos;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface ICnpjApiService
    {
        CnpjApi CompleteCustomerApi(string cnpj);
    }
}