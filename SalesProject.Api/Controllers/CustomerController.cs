using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Application.ViewModels.Customer;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICnpjApiService _cnpjApiService;

        public CustomerController(
            ICustomerService customerService,
            ICnpjApiService cnpjApiService)
        {
            _customerService = customerService;
            _cnpjApiService = cnpjApiService;
        }

        /// <summary>
        /// Get all Customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("api/[controller]")]
        public IActionResult GetCustomers()
        {
            var customers = _customerService.GetAll();

            if (customers.Any())
                return Ok(customers);

            return NotFound($"Ops. Nenhum Cliente foi encontrado.");
        }


        /// <summary>
        /// Get Customer by Id.
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
        public IActionResult GetCustomer(Guid id)
        {
            var customer = _customerService.Get(id);

            if (customer is not null)
                return Ok(customer);

            return NotFound($"Ops. Cliente com Id:'{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get Customer by Id with Adresses, Contacts and Products.
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
        [Route("api/full/[controller]/{id:guid}")]
        public IActionResult GetFullCustomer(Guid id)
        {
            var customer = _customerService.GetFullCustomer(id);

            if (customer is not null)
                return Ok(customer);

            return NotFound($"Ops. Cliente com Id:'{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get Customer by Id with Adresses and Contacts.
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
        [Route("api/complete/[controller]/{id:guid}")]
        public IActionResult GetCompleteCustomer(Guid id)
        {
            var customer = _customerService.GetCustomerWithAdressesAndContacts(id);

            if (customer is not null)
                return Ok(customer);

            return NotFound($"Ops. Cliente com Id:'{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get Customers that CompanyName contains this param.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/name/{name}")]
        public IActionResult GetCustomersByName(string name)
        {
            var customers = _customerService.GetByName(name);

            if (customers.Count > 0)
                return Ok(customers);

            return NotFound($"Ops. Nenhum cliente com nome:'{name}' foi encontrado.");
        }

        /// <summary>
        /// Get fields to complete Customer registration using a WebService (ReceitaWS).
        /// </summary>
        /// <param name="customerCnpj"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/cnpj/{customerCnpj}")]
        public IActionResult CompleteCustomerApi(string customerCnpj)
        {
            var customerResponse = _cnpjApiService.CompleteCustomerApi(customerCnpj);

            if (customerResponse.Status == "ERROR" &&
                customerResponse.Message == "CNPJ inválido")
                return BadRequest($"Ops. O cnpj:'{customerCnpj}' é inválido.");

            if (customerResponse.Status == "ERROR" &&
                customerResponse.Message == "CNPJ rejeitado pela Receita Federal")
                return NotFound($"Ops. Nenhuma empresa com cnpj:'{customerCnpj.Replace("%2F", "/")}' foi encontrada.");

            return Ok(customerResponse);
        }

        /// <summary>
        /// Create a Customer without Adresses and Contacts.
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("api/[controller]")]
        public IActionResult CreateCustomer(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool hasConflict = false;
            var customer = _customerService.Create(model, ref hasConflict);

            if (hasConflict)
                return Conflict($"Ops. O cliente com Cnpj '{model.Cnpj}' já possuí um cadastro no sistema.");

            if (!customer.Valid)
                return ValidationProblem(customer.GetAllNotifications());

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{customer.Id}",
            customer);
        }

        /// <summary>
        /// Create a Customer with Adresses and Contacts.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created Customer</returns>
        /// <response code="201">Returns the newly created customer</response>
        /// <response code="400">If the customer is invalid</response> 
        [HttpPost]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("api/complete/[controller]")]
        public IActionResult CreateCompleteCustomer(CreateCompleteCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool hasConflict = false;
            var customer = _customerService.CreateCustomerWithAdressesAndContacts(model, ref hasConflict);

            if (hasConflict)
                return Conflict($"Ops. O cliente com Cnpj '{model.Cnpj}' já possuí um cadastro no sistema.");

            if (!customer.Valid)
                return ValidationProblem(customer.GetAllNotifications());

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{customer.Id}",
            customer);
        }

        /// <summary>
        /// Delete a Customer by Id.
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
        public IActionResult DeleteCustomer(Guid id)
        {
            if (_customerService.Delete(id))
                return Ok();

            return NotFound($"Ops. Cliente com Id: '{id}' não foi encontrado.");
        }

        /// <summary>
        /// Update a Customer without Adresses and Contacts.
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
        public IActionResult EditCustomer(Guid id, EditCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCustomer = _customerService.Edit(id, model);

            if (updatedCustomer is null)
                return NotFound($"Ops. Cliente com Id: '{id}' não foi encontrado.");

            if (!updatedCustomer.Valid)
                return ValidationProblem(updatedCustomer.GetAllNotifications());

            return Ok(updatedCustomer);
        }

        /// <summary>
        /// Update a Customer with Adresses and Contacts.
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
        [Route("api/complete/[controller]/{id:guid}")]
        public IActionResult EditCompleteCustomer(Guid id, EditCompleteCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCustomer = _customerService.EditCustomerWithAdressesAndContacts(id, model);

            if (updatedCustomer is null)
                return NotFound($"Ops. Cliente com Id: '{id}' não foi encontrado.");

            if (!updatedCustomer.Valid)
                return ValidationProblem(updatedCustomer.GetAllNotifications());

            return Ok(updatedCustomer);
        }
    }
}