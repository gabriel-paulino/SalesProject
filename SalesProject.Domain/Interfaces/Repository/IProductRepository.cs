using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IProductRepository : IDisposable
    {
        Product Get(Guid id);
        List<Product> GetByName(string name);
        List<Product> GetByCustomerId(Guid customerId);
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}