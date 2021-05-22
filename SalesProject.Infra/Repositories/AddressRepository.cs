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
        private readonly DataContext _context;
        private bool _disposed = false;

        public AddressRepository(DataContext context) =>
            _context = context;

        ~AddressRepository() =>
            Dispose();

        public void Create(Address address) =>
            _context.Addreses.Add(address);

        public void Delete(Address address) =>
            _context.Addreses.Remove(address);

        public Address Get(Guid id) =>
            _context.Addreses.Find(id);

        public List<Address> GetByDescription(string description) =>
            _context.Addreses.Where(x => x.Description.Contains(description)).ToList();

        public List<Address> GetByCity(string city) =>
            _context.Addreses.Where(x => x.City.Contains(city)).ToList();

        public List<Address> GetByCustomerId(Guid customerId) =>
            _context.Addreses.Where(x => x.CustomerId.Equals(customerId)).ToList();

        public void Update(Address address) =>
            _context.Entry<Address>(address).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}