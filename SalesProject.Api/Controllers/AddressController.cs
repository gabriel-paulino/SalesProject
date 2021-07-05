using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Address;
using SalesProject.Domain.Services;
using System;
using System.Linq;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IAddressApiService _addressApiService;

        public AddressController(
            IAddressService addressService,
            IAddressApiService addressApiService)
        {
            _addressService = addressService;
            _addressApiService = addressApiService;
        }

        /// <summary>
        /// Get an Address by Id.
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
        public IActionResult GetAddress(Guid id)
        {
            var address = _addressService.Get(id);

            if (address is not null)
                return Ok(address);

            return NotFound($"Ops. Endereço com Id: '{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get all adresses with Description contains this param.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/description/{description}")]
        public IActionResult GetAddresesByDescription(string description)
        {
            var adresses = _addressService.GetByDescription(description);

            if (adresses.Any())
                return Ok(adresses);

            return NotFound($"Ops. Nenhum endereço com descrição: '{description}' foi encontrado.");
        }

        /// <summary>
        /// Get all adresses that City contains this param.
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/city/{city}")]
        public IActionResult GetAddresesByCity(string city)
        {
            var adresses = _addressService.GetByCity(city);

            if (adresses.Any())
                return Ok(adresses);

            return NotFound($"Ops. Nenhum endereço da cidade: '{city}' foi encontrado.");
        }

        /// <summary>
        /// Get fields to complete Address registration using a WebService (Viacep).
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("api/[controller]/zipcode/{zipCode}")]
        public IActionResult CompleteAddressApi(string zipCode)
        {
            var addressResponse = _addressApiService.CompleteAddressApi(zipCode);

            if (addressResponse is null)
                return BadRequest($"Ops. Algo deu errado com o cep:'{zipCode}'.");

            return Ok(addressResponse);
        }

        /// <summary>
        /// Get all Adresses of an specific Customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/customer/{customerId:guid}")]
        public IActionResult GetAddresesByCustomerId(Guid customerId)
        {
            var adresses = _addressService.GetByCustomerId(customerId);

            if (adresses.Any())
                return Ok(adresses);

            return NotFound($"Ops. Nenhum cliente com Id: '{customerId}' foi encontrado.");
        }

        /// <summary>
        /// Create an Address.
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
        public IActionResult CreateAddress(CreateAddressViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var address = _addressService.Create(model);

            if (!address.Valid)
                return ValidationProblem(address.GetAllNotifications());

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
        [Authorize(Roles = "Seller,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult DeleteAddress(Guid id)
        {
            if (_addressService.Delete(id))
                return Ok();

            return NotFound($"Ops. Endereço com Id: '{id}' não foi encontrado.");
        }

        /// <summary>
        /// Update an Address by Id.
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
        public IActionResult EditAddress(Guid id, EditAddressViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedAddress = _addressService.Edit(id, model);

            if (updatedAddress is null)
                return NotFound($"Ops. Endereço com Id: '{id}' não foi encontrado.");

            if (!updatedAddress.Valid)
                return ValidationProblem(updatedAddress.GetAllNotifications());

            return Ok(updatedAddress);
        }
    }
}