using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Product;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _uow;

        public ProductController(
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _uow = uow;
        }

        [HttpGet]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productRepository.Get(id);

            if (product != null)
                return Ok(product);

            return NotFound($"Ops. Produto com Id:'{id}' não foi encontrado.");
        }

        [HttpGet]
        [Route("api/[controller]/name/{name}")]
        public IActionResult GetProductsByName(string name)
        {
            var products = _productRepository.GetByName(name);

            if (products.Count > 0)
                return Ok(products);

            return NotFound($"Ops. Nenhum produto com nome:'{name}' foi encontrado.");
        }

        [HttpGet]
        [Route("api/[controller]/customer/{customerId:guid}")]
        public IActionResult GetProductsByCustomerId(Guid customerId)
        {
            var customer = _customerRepository.Get(customerId);

            if (customer != null)
            {
                var products = _productRepository.GetByCustomerId(customerId);

                if (products.Count > 0)
                    return Ok(products);

                return NotFound($"Ops. Nenhum produto do cliente:'{customer.CompanyName}' foi encontrado.");
            }
            return NotFound($"Ops. Nenhum cliente com Id:'{customerId}' foi encontrado.");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult CreateProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
                return ValidationProblem(detail: $"{product.GetNotification()}");

            _productRepository.Create(product);
            _uow.Commit();

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{product.Id}",
            product);
        }

        [HttpDelete]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _productRepository.Get(id);

            if (product == null)
                return NotFound($"Ops. Produto com Id:'{id}' não foi encontrado.");

            _productRepository.Delete(product);
            _uow.Commit();

            return Ok();
        }

        [HttpPatch]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult EditProduct(Guid id, EditProductViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldProduct = _productRepository.Get(id);

            if (oldProduct == null)
                return NotFound($"Ops. Produto com Id:'{id}' não foi encontrado.");

            var newProduct = oldProduct.
                        Edit(
                        name: model.Name,
                        ncmCode: model.NcmCode,
                        combinedPrice: model.CombinedPrice,
                        additionalCosts: model.AdditionalCosts,
                        combinedQuantity: model.CombinedQuantity,
                        details: model.Details);

            if (!newProduct.Valid)
                return ValidationProblem(detail: $"{newProduct.GetNotification()}");

            _productRepository.Update(newProduct);
            _uow.Commit();

            return Ok(newProduct);
        }
    }
}