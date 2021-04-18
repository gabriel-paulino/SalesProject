using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IAddressRepository : IDisposable
    {
        Address Get(Guid id);
        List<Address> GetByDescription(string description);
        List<Address> GetByCity(string name);
        List<Address> GetByCustomerId(Guid customerId);
        void Create(Address address);
        void Update(Address address);
        void Delete(Address address);
    }
}