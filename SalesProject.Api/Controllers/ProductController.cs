using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Product;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;
using System.Net.Mime;

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

        /// <summary>
        /// Get Product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productRepository.Get(id);

            if (product != null)
                return Ok(product);

            return NotFound($"Ops. Produto com Id:'{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get Produtcs that name contains this param.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/name/{name}")]
        public IActionResult GetProductsByName(string name)
        {
            var products = _productRepository.GetByName(name);

            if (products.Count > 0)
                return Ok(products);

            return NotFound($"Ops. Nenhum produto com nome:'{name}' foi encontrado.");
        }

        /// <summary>
        /// Get all Products of an specific Customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Create a Product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                return ValidationProblem($"{product.GetNotification()}");

            _productRepository.Create(product);
            _uow.Commit();

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{product.Id}",
            product);
        }

        /// <summary>
        /// Delete a Product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Update a Product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                return ValidationProblem($"{newProduct.GetNotification()}");

            _productRepository.Update(newProduct);
            _uow.Commit();

            return Ok(newProduct);
        }
    }
}