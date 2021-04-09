using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesProject.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public CustomerRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        ~CustomerRepository() =>
            Dispose();

        public async Task Create(Customer customer) =>
            await _dataContext.Customers.AddAsync(customer);

        public void Delete(Guid id)
        {
            var customer = _dataContext.Customers.Find(id);
            _dataContext.Customers.Remove(customer);
        }

        public Customer Get(Guid id) =>
            _dataContext.Customers.Find(id);

        public List<Customer> GetAll() =>
            _dataContext.Customers.ToList();

        public List<Customer> GetByName(string name) =>
            _dataContext.Customers.Where(x => x.CompanyName.Contains(name)).ToList();

        public void Update(Customer customer) =>
            _dataContext.Entry<Customer>(customer).State = EntityState.Modified;
        
        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}