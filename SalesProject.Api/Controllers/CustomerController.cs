using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Customer;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Services;
using System;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICnpjApiService _cnpjApiService;
        private readonly IUnitOfWork _uow;

        public CustomerController(
            ICustomerRepository customerRepository,
            ICnpjApiService cnpjApiService,
            IUnitOfWork uow)
        {
            _customerRepository = customerRepository;
            _cnpjApiService = cnpjApiService;
            _uow = uow;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetCustomers() =>
            Ok(_customerRepository.GetAll());

        [HttpGet]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetCustomer(Guid id)
        {
            var customer = _customerRepository.Get(id);

            if (customer != null)
                return Ok(customer);

            return NotFound($"Ops. Cliente com Id:'{id}' não foi encontrado.");
        }

        [HttpGet]
        [Route("api/full/[controller]/{id:guid}")]
        public IActionResult GetFullCustomer(Guid id)
        {
            var customer = _customerRepository.GetFullCustomer(id);

            if (customer != null)
                return Ok(customer);

            return NotFound($"Ops. Cliente com Id:'{id}' não foi encontrado.");
        }

        [HttpGet]
        [Route("api/[controller]/name/{name}")]
        public IActionResult GetCustomersByName(string name)
        {
            var customers = _customerRepository.GetByName(name);

            if (customers.Count > 0)
                return Ok(customers);

            return NotFound($"Ops. Nenhum cliente com nome:'{name}' foi encontrado.");
        }

        [HttpGet]
        [Route("api/[controller]/cnpj/{customerCnpj}")]
        public IActionResult CompleteCustomerApi(string customerCnpj)
        {
            var customerResponse = _cnpjApiService.CompleteCustomerApi(customerCnpj);

            if (customerResponse.Status == "ERROR" &&
                customerResponse.Message == "CNPJ inválido")
                return BadRequest($"Ops. O cnpj:'{customerCnpj}' é inválido.");

            if (customerResponse.Status == "ERROR" &&
                customerResponse.Message == "CNPJ rejeitado pela Receita Federal")
                return NotFound($"Ops. Nenhuma empresa com cnpj:'{customerCnpj}' foi encontrada.");

            return Ok(customerResponse);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult CreateCustomer(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer =
                    new Customer(
                        cnpj: model.Cnpj,
                        companyName: model.CompanyName,
                        opening: DateTime.Parse(model.Opening).Date,
                        phone: model.Phone,
                        municipalRegistration: model.MunicipalRegistration,
                        stateRegistration: model.StateRegistration);

            if (!customer.Valid)
                return ValidationProblem(detail: $"{customer.GetNotification()}");

            _customerRepository.Create(customer);
            _uow.Commit();

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{customer.Id}",
            customer);
        }

        [HttpDelete]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var customer = _customerRepository.Get(id);

            if (customer == null)
                return NotFound($"Ops. Cliente com Id:'{id}' não foi encontrado.");

            _customerRepository.Delete(customer);
            _uow.Commit();

            return Ok();
        }

        [HttpPatch]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult EditCustomer(Guid id, EditCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldCustomer = _customerRepository.Get(id);

            if (oldCustomer == null)
                return NotFound($"Ops. Cliente com Id:'{id}' não foi encontrado.");

            var newCustomer = oldCustomer.
                        Edit(
                        phone: model.Phone,
                        municipalRegistration: model.MunicipalRegistration,
                        stateRegistration: model.StateRegistration);

            if (!newCustomer.Valid)
                return ValidationProblem(detail: $"{newCustomer.GetNotification()}");

            _customerRepository.Update(newCustomer);
            _uow.Commit();

            return Ok(newCustomer);
        }
    }
}