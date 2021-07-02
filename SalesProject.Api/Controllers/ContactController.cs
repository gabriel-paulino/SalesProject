using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Contact;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _uow;

        public ContactController(
            IContactRepository contactRepository,
            ICustomerRepository customerRepository,
            IUnitOfWork uow)
        {
            _contactRepository = contactRepository;
            _customerRepository = customerRepository;
            _uow = uow;
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
            var contact = _contactRepository.Get(id);

            if (contact is not null)
                return Ok(contact);

            return NotFound($"Ops. Contato com Id:'{id}' não foi encontrado.");
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
            var contacts = _contactRepository.GetByName(name);

            if (contacts.Count > 0)
                return Ok(contacts);

            return NotFound($"Ops. Nenhum contato com nome:'{name}' foi encontrado.");
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
            var customer = _customerRepository.Get(customerId);

            if (customer is not null)
            {
                var contacts = _contactRepository.GetByCustomerId(customerId);

                if (contacts.Count > 0)
                    return Ok(contacts);

                return NotFound($"Ops. Nenhum contato do cliente:'{customer.CompanyName}' foi encontrado.");
            }
            return NotFound($"Ops. Nenhum cliente com Id:'{customerId}' foi encontrado.");
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

            var contact =
                    new Contact(
                        firstName: model.FirstName,
                        lastName: model.LastName,
                        email: model.Email,
                        whatsApp: model.WhatsApp,
                        phone: model.Phone,
                        customerId: Guid.Parse(model.CustomerId));

            if (!contact.Valid)
                return ValidationProblem($"{contact.GetNotification()}");

            _contactRepository.Create(contact);
            _uow.Commit();

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
            var contact = _contactRepository.Get(id);

            if (contact is null)
                return NotFound($"Ops. Contato com Id:'{id}' não foi encontrado.");

            _contactRepository.Delete(contact);
            _uow.Commit();

            return Ok();
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

            var oldContact = _contactRepository.Get(id);

            if (oldContact is null)
                return NotFound($"Ops. Contato com Id:'{id}' não foi encontrado.");

            var newContact = oldContact.
                        Edit(
                        firstName: model.FirstName,
                        lastName: model.LastName,
                        email: model.Email,
                        whatsApp: model.WhatsApp,
                        phone: model.Phone);

            if (!newContact.Valid)
                return ValidationProblem($"{newContact.GetNotification()}");

            _contactRepository.Update(newContact);
            _uow.Commit();

            return Ok(newContact);
        }
    }
}