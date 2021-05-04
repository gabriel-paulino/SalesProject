using SalesProject.Domain.Dtos;

namespace SalesProject.Domain.Services
{
    public interface ICnpjApiService
    {
        CnpjApi CompleteCustomerApi(string cnpj);
    }
}