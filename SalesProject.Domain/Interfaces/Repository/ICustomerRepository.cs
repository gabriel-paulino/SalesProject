using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface ICustomerRepository : IDisposable
    {
        ICollection<Customer> GetAll();
        Customer Get(Guid id);
        Customer GetFullCustomer(Guid id);
        Customer GetCustomerWithAdressesAndContacts(Guid id);
        ICollection<Customer> GetByName(string name);
        bool HasAnotherCustomerWithThisCnpj(string cnpj);
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}