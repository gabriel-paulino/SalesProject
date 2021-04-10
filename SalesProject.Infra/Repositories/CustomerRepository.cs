using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Create(Customer customer) =>
             _dataContext.Customers.Add(customer);

        public void Delete(Customer customer) =>
            _dataContext.Customers.Remove(customer);

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