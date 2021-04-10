using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IAddressRepository : IDisposable
    {
        List<Address> GetAll();
        Address Get(Guid id);
        List<Address> GetByCity(string name);
        void Create(Address address);
        void Update(Address address);
        void Delete(Address address);
    }
}