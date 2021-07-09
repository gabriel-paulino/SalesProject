using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Contact;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(
            IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Get a Contact by Id.
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
        public IActionResult GetContact(Guid id)
        {
            var contact = _contactService.Get(id);

            if (contact is not null)
                return Ok(contact);

            return NotFound($"Ops. Contato com Id: '{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get all contacts with FullName contains this param.
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
        public IActionResult GetContactsByName(string name)
        {
            var contacts = _contactService.GetByName(name);

            if (contacts.Any())
                return Ok(contacts);

            return NotFound($"Ops. Nenhum contato com nome: '{name}' foi encontrado.");
        }

        /// <summary>
        /// Get all Contacts of an specific Customer.
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
        public IActionResult GetContactsByCustomerId(Guid customerId)
        {
            var contacts = _contactService.GetByCustomerId(customerId);

            if (contacts.Any())
                return Ok(contacts);

            return NotFound($"Ops. Nenhum Contato com ClienteId: '{customerId}' foi encontrado.");
        }

        /// <summary>
        /// Create a Contact.
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
        public IActionResult CreateContact(CreateContactViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contact = _contactService.Create(model);

            if (!contact.Valid)
                return ValidationProblem(contact.GetAllNotifications());

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{contact.Id}",
            contact);
        }

        /// <summary>
        /// Delete a Contact by Id.
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
        public IActionResult DeleteContact(Guid id)
        {
            if (_contactService.Delete(id))
                return Ok();

            return NotFound($"Ops. Contato com Id: '{id}' não foi encontrado.");
        }

        /// <summary>
        /// Update a Contact by Id.
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
        public IActionResult EditContact(Guid id, EditContactViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedContact = _contactService.Edit(id, model);

            if (updatedContact is null)
                return NotFound($"Ops. Contato com Id: '{id}' não foi encontrado.");

            if (!updatedContact.Valid)
                return ValidationProblem(updatedContact.GetAllNotifications());

            return Ok(updatedContact);
        }
    }
}