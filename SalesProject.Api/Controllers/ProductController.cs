using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Application.ViewModels.Product;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(
            IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get Product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productService.Get(id);

            if (product is not null)
                return Ok(product);

            return NotFound($"Ops. Produto com Id: '{id}' não existe");
        }

        /// <summary>
        /// Get Produtcs that name contains this param.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Customer,Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/name/{name}")]
        public IActionResult GetProductsByName(string name)
        {
            var products = _productService.GetByName(name);

            if (products.Any())
                return Ok(products);

            return NotFound($"Ops. Nenhum produto com nome: '{name}' foi encontrado.");
        }

        /// <summary>
        /// Get all Products of an specific Customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/customer/{customerId:guid}")]
        public IActionResult GetProductsByCustomerId(Guid customerId)
        {
            var products = _productService.GetByCustomerId(customerId);

            if (products.Any())
                return Ok(products);

            return NotFound($"Ops. Nenhum produto com ClienteId: '{customerId}' foi encontrado.");
        }

        /// <summary>
        /// Create a Product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("api/[controller]")]
        public IActionResult CreateProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _productService.Create(model);

            if (!product.Valid)
                return ValidationProblem(product.GetAllNotifications());

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
        [Authorize(Roles = "Seller,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            if (_productService.Delete(id))
                return Ok();

            return NotFound($"Ops. Produto com Id: '{id}' não existe.");
        }

        /// <summary>
        /// Update a Product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult EditProduct(Guid id, EditProductViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProduct = _productService.Edit(id, model);

            if (updatedProduct is null)
                return NotFound($"Ops. Produto com Id: '{id}' não existe.");

            if (!updatedProduct.Valid)
                return ValidationProblem(updatedProduct.GetAllNotifications());

            return Ok(updatedProduct);
        }
    }
}