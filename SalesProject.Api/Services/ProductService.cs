using SalesProject.Api.ViewModels.Product;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace SalesProject.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;

        public ProductService(
            IProductRepository productRepository,
            IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _uow = uow;
        }

        public Product Create(object createProductViewModel)
        {
            var model = (CreateProductViewModel)createProductViewModel;

            var product =
                    new Product(
                        name: model.Name,
                        ncmCode: model.NcmCode,
                        combinedPrice: model.CombinedPrice,
                        additionalCosts: model.AdditionalCosts,
                        combinedQuantity: model.CombinedQuantity,
                        details: model.Details,
                        customerId: Guid.Parse(model.CustomerId));

            if (!product.Valid)
                return product;

            _productRepository.Create(product);
            _uow.Commit();

            return product;
        }

        public bool Delete(Guid id)
        {
            var product = _productRepository.Get(id);

            if (product is null)
                return false;

            _productRepository.Delete(product);
            _uow.Commit();

            return true;
        }

        public Product Edit(Guid id, object editProductViewModel)
        {
            var model = (EditProductViewModel)editProductViewModel;
            var currentProduct = _productRepository.Get(id);

            if (currentProduct is null)
                return null;

            var updatedProduct = currentProduct.
                        Edit(
                        name: model.Name,
                        ncmCode: model.NcmCode,
                        combinedPrice: model.CombinedPrice,
                        additionalCosts: model.AdditionalCosts,
                        combinedQuantity: model.CombinedQuantity,
                        details: model.Details);

            if (!updatedProduct.Valid)
                return updatedProduct;

            _productRepository.Update(updatedProduct);
            _uow.Commit();

            return updatedProduct;
        }

        public Product Get(Guid id) =>
            _productRepository.Get(id);

        public ICollection<Product> GetByCustomerId(Guid customerId) =>
            _productRepository.GetByCustomerId(customerId);

        public ICollection<Product> GetByName(string name) =>
            _productRepository.GetByName(name);
    }
}
