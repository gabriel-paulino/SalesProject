using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Contact;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _uow;

        public ContactController(
            IContactRepository contactRepository,
            IUnitOfWork uow)
        {
            _contactRepository = contactRepository;
            _uow = uow;
        }

        [HttpGet]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetContact(Guid id)
        {
            var contact = _contactRepository.Get(id);

            if (contact != null)
                return Ok(contact);

            return NotFound($"Ops. Contato com Id:'{id}' não foi encontrado.");
        }

        [HttpGet]
        [Route("api/[controller]/name/{name}")]
        public IActionResult GetContactByName(string name)
        {
            var contacts = _contactRepository.GetByName(name);

            if (contacts.Count > 0)
                return Ok(contacts);

            return NotFound($"Ops. Nenhum contato com nome:'{name}' foi encontrado.");
        }

        [HttpPost]
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
                return ValidationProblem(detail: $"{contact.GetNotification()}");

            _contactRepository.Create(contact);
            _uow.Commit();

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{contact.Id}",
            contact);
        }

        [HttpDelete]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = _contactRepository.Get(id);

            if (contact == null)
                return NotFound($"Ops. Contato com Id:'{id}' não foi encontrado.");

            _contactRepository.Delete(contact);
            _uow.Commit();

            return Ok();
        }

        [HttpPatch]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult EditContact(Guid id, EditContactViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldContact = _contactRepository.Get(id);

            if (oldContact == null)
                return NotFound($"Ops. Contato com Id:'{id}' não foi encontrado.");

            var newContact = oldContact.
                        Edit(
                        firstName: model.FirstName,
                        lastName: model.LastName,
                        email: model.Email,
                        whatsApp: model.WhatsApp,
                        phone: model.Phone);

            if (!newContact.Valid)
                return ValidationProblem(detail: $"{newContact.GetNotification()}");

            _contactRepository.Update(newContact);
            _uow.Commit();

            return Ok(newContact);
        }
    }
}