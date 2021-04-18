using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _dataContext.Addreses.Add(address);

        public void Delete(Address address) =>
            _dataContext.Addreses.Remove(address);

        public Address Get(Guid id) =>
            _dataContext.Addreses.Find(id);

        public List<Address> GetByDescription(string description) =>
            _dataContext.Addreses.Where(x => x.Description.Contains(description)).ToList();

        public List<Address> GetByCity(string city) =>
            _dataContext.Addreses.Where(x => x.City.Contains(city)).ToList();

        public List<Address> GetByCustomerId(Guid customerId) =>
            _dataContext.Addreses.Where(x => x.CustomerId.Equals(customerId)).ToList();

        public void Update(Address address) =>
            _dataContext.Entry<Address>(address).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }  
    }
}