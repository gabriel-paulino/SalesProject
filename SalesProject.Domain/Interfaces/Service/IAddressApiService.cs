using SalesProject.Domain.Dtos;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IAddressApiService
    {
        AddressApi CompleteAddressApi(string zipCode);
    }
}