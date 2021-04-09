using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;

namespace SalesProject.Infra.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public AddressRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        ~AddressRepository() =>
            Dispose();

        public void Create(Address address) =>
             _dataContext.Adresses.Add(address);

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}