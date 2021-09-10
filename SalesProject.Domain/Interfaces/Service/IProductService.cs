using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IProductService
    {
        Product Get(Guid id);
        ICollection<Product> GetByName(string name);
        ICollection<Product> GetByCustomerId(Guid customerId);
        Product Create(object createProductViewModel);
        bool Delete(Guid id);
        Product Edit(Guid id, object editProductViewModel);
    }
}