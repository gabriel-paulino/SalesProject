using SalesProject.Domain.Dtos;

namespace SalesProject.Domain.Services
{
    public interface IAddressApiService
    {
        AddressApi CompleteAddressApi(string zipCode);
    }
}