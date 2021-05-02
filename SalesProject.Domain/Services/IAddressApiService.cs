using SalesProject.Domain.Entities;

namespace SalesProject.Domain.Services
{
    public interface IAddressApiService
    {
        AddressApi CompleteAddressApi(string zipCode);
    }
}