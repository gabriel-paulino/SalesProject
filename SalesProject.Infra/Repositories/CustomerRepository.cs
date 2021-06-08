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
        private readonly DataContext _context;
        private bool _disposed = false;

        public CustomerRepository(DataContext context) =>
            _context = context;

        ~CustomerRepository() =>
            Dispose();

        public void Create(Customer customer) =>
             _context.Customers.Add(customer);

        public void Delete(Customer customer) =>
            _context.Customers.Remove(customer);

        public Customer Get(Guid id) =>
            _context.Customers.Find(id);

        public Customer GetFullCustomer(Guid id) =>
            _context.Customers
                .AsNoTracking()
                .Include(c => c.Adresses)
                .Include(c => c.Contacts)
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);

        public Customer GetCompleteCustomer(Guid id) =>
            _context.Customers
                .Include(c => c.Adresses)
                .Include(c => c.Contacts)
                .FirstOrDefault(c => c.Id == id);

        public List<Customer> GetAll() =>
            _context.Customers.ToList();

        public List<Customer> GetByName(string name) =>
            _context.Customers.Where(x => x.CompanyName.Contains(name)).ToList();

        public bool HasAnotherCustomerWithThisCnpj(string cnpj) =>
            _context.Customers.Where(x => x.Cnpj == cnpj).Any();

        public void Update(Customer customer) =>
            _context.Entry<Customer>(customer).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}