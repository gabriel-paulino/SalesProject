using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public ProductRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        ~ProductRepository() =>
            Dispose();

        public void Create(Product product) =>
            _dataContext.Products.Add(product);

        public void Delete(Product product) =>
            _dataContext.Products.Remove(product);

        public Product Get(Guid id) =>
            _dataContext.Products.Find(id);

        public List<Product> GetAll() =>
            _dataContext.Products.ToList();

        public List<Product> GetByName(string name) =>
            _dataContext.Products.Where(x => x.Name.Contains(name)).ToList();

        public void Update(Product product) =>
            _dataContext.Entry<Product>(product).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}