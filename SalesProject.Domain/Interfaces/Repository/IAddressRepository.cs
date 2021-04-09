using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IAddressRepository : IDisposable
    {
        void Create(Address address);
    }
}