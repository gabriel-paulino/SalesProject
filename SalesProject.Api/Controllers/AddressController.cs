using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Address;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _uow;

        public AddressController(
            IAddressRepository addressRepository,
            ICustomerRepository customerRepository,
            IUnitOfWork uow)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _uow = uow;
        }

        [HttpGet]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetAddress(Guid id)
        {
            var address = _addressRepository.Get(id);

            if (address != null)
                return Ok(address);

            return NotFound($"Ops. Endereço com Id:'{id}' não foi encontrado.");
        }

        [HttpGet]
        [Route("api/[controller]/description/{description}")]
        public IActionResult GetAddresesByDescription(string description)
        {
            var addreses = _addressRepository.GetByDescription(description);

            if (addreses.Count > 0)
                return Ok(addreses);

            return NotFound($"Ops. Nenhum endereço com descrição:'{description}' foi encontrado.");
        }

        [HttpGet]
        [Route("api/[controller]/city/{city}")]
        public IActionResult GetAddresesByCity(string city)
        {
            var addreses = _addressRepository.GetByCity(city);

            if (addreses.Count > 0)
                return Ok(addreses);

            return NotFound($"Ops. Nenhum endereço da cidade:'{city}' foi encontrado.");
        }

        [HttpGet]
        [Route("api/[controller]/customer/{customerId:guid}")]
        public IActionResult GetAddresesByCustomerId(Guid customerId)
        {
            var customer = _customerRepository.Get(customerId);

            if (customer != null)
            {
                var addreses = _addressRepository.GetByCustomerId(customerId);

                if (addreses.Count > 0)
                    return Ok(addreses);

                return NotFound($"Ops. Nenhum endereço do cliente:'{customer.CompanyName}' foi encontrado.");
            }
            return NotFound($"Ops. Nenhum cliente com Id:'{customerId}' foi encontrado.");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult CreateAddress(CreateAddressViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var address =
                    new Address(
                        description: model.Description,
                        zipCode: model.ZipCode,
                        type: (Domain.Enums.AddressType)model.Type,
                        street: model.Street,
                        neighborhood: model.Neighborhood,
                        number: model.Number,
                        city: model.City,
                        state: model.State,
                        customerId: Guid.Parse(model.CustomerId));

            if (!address.Valid)
                return ValidationProblem(detail: $"{address.GetNotification()}");

            _addressRepository.Create(address);
            _uow.Commit();

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{address.Id}",
            address);
        }

        [HttpDelete]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult DeleteAddress(Guid id)
        {
            var address = _addressRepository.Get(id);

            if (address == null)
                return NotFound($"Ops. Endereço com Id:'{id}' não foi encontrado.");

            _addressRepository.Delete(address);
            _uow.Commit();

            return Ok();
        }

        [HttpPatch]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult EditAddress(Guid id, EditAddressViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldAddress = _addressRepository.Get(id);

            if (oldAddress == null)
                return NotFound($"Ops. Endereço com Id:'{id}' não foi encontrado.");

            var newAddress = oldAddress.
                        Edit(
                        description: model.Description,
                        zipCode: model.ZipCode,
                        type: (Domain.Enums.AddressType)model.Type,
                        street: model.Street,
                        neighborhood: model.Neighborhood,
                        number: model.Number,
                        city: model.City,
                        state: model.State);

            if (!newAddress.Valid)
                return ValidationProblem(detail: $"{newAddress.GetNotification()}");

            _addressRepository.Update(newAddress);
            _uow.Commit();

            return Ok(newAddress);
        }
    }
}