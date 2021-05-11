﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Address;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Services;
using System;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressApiService _addressApiService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _uow;

        public AddressController(
            IAddressRepository addressRepository,
            IAddressApiService addressApiService,
            ICustomerRepository customerRepository,
            IUnitOfWork uow)
        {
            _addressRepository = addressRepository;
            _addressApiService = addressApiService;
            _customerRepository = customerRepository;
            _uow = uow;
        }

        /// <summary>
        /// Get an Address by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetAddress(Guid id)
        {
            var address = _addressRepository.Get(id);

            if (address != null)
                return Ok(address);

            return NotFound($"Ops. Endereço com Id:'{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get all adresses with Description contains this param.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/description/{description}")]
        public IActionResult GetAddresesByDescription(string description)
        {
            var adresses = _addressRepository.GetByDescription(description);

            if (adresses.Count > 0)
                return Ok(adresses);

            return NotFound($"Ops. Nenhum endereço com descrição:'{description}' foi encontrado.");
        }

        /// <summary>
        /// Get all adresses that City contains this param.
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/city/{city}")]
        public IActionResult GetAddresesByCity(string city)
        {
            var adresses = _addressRepository.GetByCity(city);

            if (adresses.Count > 0)
                return Ok(adresses);

            return NotFound($"Ops. Nenhum endereço da cidade:'{city}' foi encontrado.");
        }

        /// <summary>
        /// Get fields to complete Address registration using a WebService (ApiCep).
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/zipcode/{zipCode}")]
        public IActionResult CompleteAddressApi(string zipCode)
        {
            var addressResponse = _addressApiService.CompleteAddressApi(zipCode);

            if (addressResponse.Status == 400)
                return BadRequest($"Ops. O cep:'{zipCode}' é inválido.");

            if (addressResponse.Status == 404)
                return NotFound($"Ops. Nenhum endereço para o cep:'{zipCode}' foi encontrado.");

            return Ok(addressResponse);
        }

        /// <summary>
        /// Get all Adresses of an specific Customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/customer/{customerId:guid}")]
        public IActionResult GetAddresesByCustomerId(Guid customerId)
        {
            var customer = _customerRepository.Get(customerId);

            if (customer != null)
            {
                var adresses = _addressRepository.GetByCustomerId(customerId);

                if (adresses.Count > 0)
                    return Ok(adresses);

                return NotFound($"Ops. Nenhum endereço do cliente:'{customer.CompanyName}' foi encontrado.");
            }
            return NotFound($"Ops. Nenhum cliente com Id:'{customerId}' foi encontrado.");
        }

        /// <summary>
        /// Create an Address.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/[controller]")]
        public IActionResult CreateAddress(CreateAddressViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var address =
                    new Address(
                        description: model.Description,
                        zipCode: model.ZipCode,
                        type: (AddressType)model.Type,
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

        /// <summary>
        /// Delete an Address by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Update an Address by Id.
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
                        type: (AddressType)model.Type,
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