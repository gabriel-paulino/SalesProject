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
        private readonly DataContext _context;
        private bool _disposed = false;

        public ProductRepository(DataContext dataContext) =>
            _context = dataContext;

        ~ProductRepository() =>
            Dispose();

        public void Create(Product product) =>
            _context.Products.Add(product);

        public void Delete(Product product) =>
            _context.Products.Remove(product);

        public Product Get(Guid id) =>
            _context.Products.Find(id);

        public ICollection<Product> GetByName(string name) =>
            _context.Products.Where(x => x.Name.Contains(name)).ToList();

        public ICollection<Product> GetByCustomerId(Guid customerId) =>
            _context.Products.Where(x => x.CustomerId.Equals(customerId)).ToList();

        public void Update(Product product) =>
            _context.Entry<Product>(product).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}