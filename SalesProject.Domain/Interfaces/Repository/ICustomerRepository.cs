using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface ICustomerRepository : IDisposable
    {
        List<Customer> GetAll();
        Customer Get(Guid id);
        List<Customer> GetByName(string name);
        Task Create(Customer customer);
        void Update(Customer customer);
        void Delete(Guid id);
    }
}