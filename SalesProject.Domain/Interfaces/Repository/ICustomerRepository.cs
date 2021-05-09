using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface ICustomerRepository : IDisposable
    {
        List<Customer> GetAll();
        Customer Get(Guid id);
        Customer GetFullCustomer(Guid id);
        Customer GetCompleteCustomer(Guid id);
        List<Customer> GetByName(string name);
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}