using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Customer;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
        [Route("api/complete/[controller]/{id:guid}")]
        public IActionResult GetCompleteCustomer(Guid id)
        {
            var customer = _customerRepository.GetCompleteCustomer(id);

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

        [HttpPost]
        [Route("api/complete/[controller]")]
        public IActionResult CreateCompleteCustomer(CreateCompleteCustomerViewModel model)
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

            foreach (var line in model.Adresses)
            {
                var address =
                    new Address(
                        description: line.Description,
                        zipCode: line.ZipCode,
                        type: (AddressType)line.Type,
                        street: line.Street,
                        neighborhood: line.Neighborhood,
                        number: line.Number,
                        city: line.City,
                        state: line.State,
                        customerId: customer.Id);

                if (!address.Valid)
                    return ValidationProblem(detail: $"{address.GetNotification()}");

                customer.AddAddress(address);
            }

            foreach (var line in model.Contacts)
            {
                var contact =
                    new Contact(firstName: line.FirstName,
                                lastName: line.LastName,
                                email: line.Email,
                                whatsApp: line.WhatsApp,
                                phone: line.Phone,
                                customerId: customer.Id);

                if (!contact.Valid)
                    return ValidationProblem(detail: $"{contact.GetNotification()}");

                customer.AddContact(contact);
            }

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

        [HttpPatch]
        [Route("api/complete/[controller]/{id:guid}")]
        public IActionResult EditCompleteCustomer(Guid id, EditCompleteCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldCustomer = _customerRepository.GetCompleteCustomer(id);

            if (oldCustomer == null)
                return NotFound($"Ops. Cliente com Id:'{id}' não foi encontrado.");

            var newCustomer = oldCustomer.
                        Edit(
                        phone: model.Phone,
                        municipalRegistration: model.MunicipalRegistration,
                        stateRegistration: model.StateRegistration);

            if (!newCustomer.Valid)
                return ValidationProblem(detail: $"{newCustomer.GetNotification()}");

            #region Manage Adresses

            List<Address> newAdresses = new List<Address>();
            List<Address> removedAdresses = new List<Address>();

            foreach (var address in oldCustomer.Adresses)
            {
                bool remove = false;

                for (int i = 0; i < model.Adresses.Count; i++)
                {
                    Guid currentIdAddress = string.IsNullOrEmpty(model.Adresses[i].Id)
                    ? new Guid()
                    : Guid.Parse(model.Adresses[i].Id);

                    if (address.Id == currentIdAddress)
                        break;

                    if (i == model.Adresses.Count - 1)
                    {
                        remove = true;
                        break;
                    }
                }
                if (remove)
                    removedAdresses.Add(address);
            }

            foreach (var addressModel in model.Adresses)
            {
                if (string.IsNullOrEmpty(addressModel.Id))
                {
                    var newAddress =
                            new Address(
                                description: addressModel.Description,
                                zipCode: addressModel.ZipCode,
                                type: (AddressType)addressModel.Type,
                                street: addressModel.Street,
                                neighborhood: addressModel.Neighborhood,
                                number: addressModel.Number,
                                city: addressModel.City,
                                state: addressModel.State,
                                customerId: oldCustomer.Id);

                    if (!newAddress.Valid)
                        return ValidationProblem(detail: $"{newAddress.GetNotification()}");

                    newAdresses.Add(newAddress);
                }
            }

            foreach (var addressModel in model.Adresses)
            {
                if (string.IsNullOrEmpty(addressModel.Id))
                    continue;

                var address =
                    newCustomer
                    .Adresses
                    .Where(c => c.Id == Guid.Parse(addressModel.Id))
                    .FirstOrDefault();

                address.Edit(
                            description: addressModel.Description,
                            zipCode: addressModel.ZipCode,
                            type: (AddressType)addressModel.Type,
                            street: addressModel.Street,
                            neighborhood: addressModel.Neighborhood,
                            number: addressModel.Number,
                            city: addressModel.City,
                            state: addressModel.State);

                if (!address.Valid)
                    return ValidationProblem(detail: $"{address.GetNotification()}");
            }

            #endregion

            #region Manage Contacts

            List<Contact> newContacts = new List<Contact>();
            List<Contact> removedContacts = new List<Contact>();

            foreach (var contact in oldCustomer.Contacts)
            {
                bool remove = false;

                for (int i = 0; i < model.Contacts.Count; i++)
                {
                    Guid currentIdContact = string.IsNullOrEmpty(model.Contacts[i].Id)
                    ? new Guid()
                    : Guid.Parse(model.Contacts[i].Id);

                    if (contact.Id == currentIdContact)
                        break;

                    if (i == model.Contacts.Count - 1)
                    {
                        remove = true;
                        break;
                    }
                }
                if (remove)
                    removedContacts.Add(contact);
            }

            foreach (var contactModel in model.Contacts)
            {
                if (string.IsNullOrEmpty(contactModel.Id))
                {
                    var newContact =
                        new Contact(
                            firstName: contactModel.FirstName,
                            lastName: contactModel.LastName,
                            email: contactModel.Email,
                            whatsApp: contactModel.WhatsApp,
                            phone: contactModel.Phone,
                            customerId: oldCustomer.Id);

                    if (!newContact.Valid)
                        return ValidationProblem(detail: $"{newContact.GetNotification()}");

                    newContacts.Add(newContact);
                }
            }

            foreach (var contactModel in model.Contacts)
            {
                if (string.IsNullOrEmpty(contactModel.Id))
                    continue;

                var contact =
                    newCustomer
                    .Contacts
                    .Where(c => c.Id == Guid.Parse(contactModel.Id))
                    .FirstOrDefault();

                contact.Edit(
                        firstName: contactModel.FirstName,
                        lastName: contactModel.LastName,
                        email: contactModel.Email,
                        whatsApp: contactModel.WhatsApp,
                        phone: contactModel.Phone);

                if (!contact.Valid)
                    return ValidationProblem(detail: $"{contact.GetNotification()}");
            }

            #endregion

            if (newAdresses.Any())
                foreach (var newAddress in newAdresses)
                    newCustomer.AddAddress(newAddress);

            if (newContacts.Any())
                foreach (var contact in newContacts)
                    newCustomer.AddContact(contact);

            if (removedAdresses.Any())
                foreach (var address in removedAdresses)
                    newCustomer.RemoveAddress(address);

            if (removedContacts.Any())
                foreach (var contact in removedContacts)
                    newCustomer.RemoveContact(contact);

            _customerRepository.Update(newCustomer);
            _uow.Commit();

            return Ok(newCustomer);
        }
    }
}