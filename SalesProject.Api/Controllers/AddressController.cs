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
        private readonly IUnitOfWork _uow;

        public AddressController(
            IAddressRepository addressRepository,
            IUnitOfWork uow)
        {
            _addressRepository = addressRepository;
            _uow = uow;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAddresses() =>
            Ok(_addressRepository.GetAll());

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
        [Route("api/[controller]/city/{city}")]
        public IActionResult GetAddressByCity(string city)
        {
            var addreses = _addressRepository.GetByCity(city);

            if (addreses.Count > 0)
                return Ok(addreses);

            return NotFound($"Ops. Nenhum endereço da cidade:'{city}' foi encontrado.");
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