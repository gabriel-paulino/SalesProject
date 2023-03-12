using SalesProject.Application.ViewModels.Customer;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Extensions;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Interfaces.Service;
using SalesProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesProject.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICachingService _cachingService;
        private readonly IUnitOfWork _uow;

        public CustomerService
        (
            ICustomerRepository customerRepository,
            ICachingService cachingService,
            IUnitOfWork uow
        )
        {
            _customerRepository = customerRepository;
            _cachingService = cachingService;
            _uow = uow;
        }

        public Customer Create(object createCustomerViewModel, ref bool hasConflit)
        {
            var model = (CreateCustomerViewModel)createCustomerViewModel;
            string cnpj = model.Cnpj.CleanCnpjToSaveDataBase();

            var customer =
                    new Customer(
                        cnpj: cnpj,
                        companyName: model.CompanyName,
                        opening: DateTime.Parse(model.Opening).Date,
                        phone: model.Phone,
                        municipalRegistration: model.MunicipalRegistration,
                        stateRegistration: model.StateRegistration,
                        email: model.Email);

            if (!customer.Valid)
                return customer;

            if (_customerRepository.HasAnotherCustomerWithThisCnpj(customer.Cnpj))
            {
                hasConflit = true;
                return customer;
            }

            _customerRepository.Create(customer);
            _uow.Commit();

            return customer;
        }

        public Customer CreateCustomerWithAdressesAndContacts(object createCompleteCustomerViewModel, ref bool hasConflit)
        {
            var model = (CreateCompleteCustomerViewModel)createCompleteCustomerViewModel;
            string cnpj = model.Cnpj.CleanCnpjToSaveDataBase();

            var customer =
                    new Customer(
                        cnpj: cnpj,
                        companyName: model.CompanyName,
                        opening: DateTime.Parse(model.Opening).Date,
                        phone: model.Phone,
                        municipalRegistration: model.MunicipalRegistration,
                        stateRegistration: model.StateRegistration,
                        email: model.Email);

            if (!customer.Valid)
                return customer;

            if (_customerRepository.HasAnotherCustomerWithThisCnpj(customer.Cnpj))
            {
                hasConflit = true;
                return customer;
            }

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
                {
                    customer.AddNotification(address.GetAllNotifications());
                    return customer;
                }

                address.SetCodeCity(codeCity: line.CodeCity);
                customer.AddAddress(address);
            }

            if (!customer.Adresses.Where(a => a.Type == AddressType.Billing).Any())
            {
                customer.AddNotification($"Ops. Para adicionar um Cliente é necesssario um Endereço do tipo Cobrança");
                return customer;
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
                {
                    customer.AddNotification(contact.GetAllNotifications());
                    return customer;
                }

                customer.AddContact(contact);
            }

            _customerRepository.Create(customer);
            _uow.Commit();

            return customer;
        }

        public bool Delete(Guid id)
        {
            var customer = _customerRepository.Get(id);

            if (customer is null)
                return false;

            _customerRepository.Delete(customer);
            _uow.Commit();

            return true;
        }

        public Customer Edit(Guid id, object editCustomerViewModel)
        {
            var model = (EditCustomerViewModel)editCustomerViewModel;
            var currentCustomer = _customerRepository.Get(id);

            if (currentCustomer is null)
                return null;

            var updatedCustomer = currentCustomer.
                        Edit(
                        phone: model.Phone,
                        municipalRegistration: model.MunicipalRegistration,
                        stateRegistration: model.StateRegistration,
                        email: model.Email);

            if (!updatedCustomer.Valid)
                return updatedCustomer;

            _customerRepository.Update(updatedCustomer);
            _uow.Commit();

            return updatedCustomer;
        }

        public Customer EditCustomerWithAdressesAndContacts(Guid id, object editCompleteCustomerViewModel)
        {
            var model = (EditCompleteCustomerViewModel)editCompleteCustomerViewModel;
            var currentCustomer = _customerRepository.GetCustomerWithAdressesAndContacts(id);

            if (currentCustomer is null)
                return null;

            var updatedCustomer = currentCustomer.
                        Edit(
                        phone: model.Phone,
                        municipalRegistration: model.MunicipalRegistration,
                        stateRegistration: model.StateRegistration,
                        email: model.Email);

            if (!updatedCustomer.Valid)
                return updatedCustomer;

            #region Manage Adresses

            List<Address> newAdresses = new List<Address>();
            List<Address> removedAdresses = new List<Address>();

            foreach (var address in currentCustomer.Adresses)
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
                                customerId: currentCustomer.Id);

                    if (!newAddress.Valid)
                    {
                        updatedCustomer.AddNotification(newAddress.GetAllNotifications());
                        return updatedCustomer;
                    }

                    newAddress.SetCodeCity(codeCity: addressModel.CodeCity);
                    newAdresses.Add(newAddress);
                }
            }

            foreach (var addressModel in model.Adresses)
            {
                if (string.IsNullOrEmpty(addressModel.Id))
                    continue;

                var address =
                    updatedCustomer
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
                {
                    updatedCustomer.AddNotification(address.GetAllNotifications());
                    return updatedCustomer;
                }

                address.SetCodeCity(codeCity: addressModel.CodeCity);
            }

            #endregion

            #region Manage Contacts

            List<Contact> newContacts = new List<Contact>();
            List<Contact> removedContacts = new List<Contact>();

            foreach (var contact in currentCustomer.Contacts)
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
                            customerId: currentCustomer.Id);

                    if (!newContact.Valid)
                    {
                        updatedCustomer.AddNotification(newContact.GetAllNotifications());
                        return updatedCustomer;
                    }

                    newContacts.Add(newContact);
                }
            }
            foreach (var contactModel in model.Contacts)
            {
                if (string.IsNullOrEmpty(contactModel.Id))
                    continue;

                var contact =
                    updatedCustomer
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
                {
                    updatedCustomer.AddNotification(contact.GetAllNotifications());
                    return updatedCustomer;
                }
            }

            #endregion

            if (newAdresses.Any())
                foreach (var newAddress in newAdresses)
                    updatedCustomer.AddAddress(newAddress);

            if (newContacts.Any())
                foreach (var contact in newContacts)
                    updatedCustomer.AddContact(contact);

            if (removedAdresses.Any())
                foreach (var address in removedAdresses)
                    updatedCustomer.RemoveAddress(address);

            if (removedContacts.Any())
                foreach (var contact in removedContacts)
                    updatedCustomer.RemoveContact(contact);

            if (!updatedCustomer.Adresses.Where(a => a.Type == AddressType.Billing).Any())
            {
                updatedCustomer.AddNotification($"Ops. Falha ao editar Cliente '{updatedCustomer.CompanyName}', deve possuir um Endereço do tipo Cobrança");
                return updatedCustomer;
            }

            _customerRepository.Update(updatedCustomer);
            _uow.Commit();

            return updatedCustomer;
        }

        public Customer Get(Guid id) =>
            _customerRepository.Get(id);

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            const string CacheKey = "CustomerList";

            var customersCache = await _cachingService
                .GetListAsync<CustomerModel>(CacheKey);

            if (customersCache.Any())
                return customersCache.Select(cache => (Customer)cache);

            var customers = await _customerRepository.GetAllAsync();

            if (customers.Any())
            {
                var model = customers.Select(customer => (CustomerModel)customer);
                await _cachingService.SetListAsync(CacheKey, model, 3600, 1200);
                return customers;
            }

            return Enumerable.Empty<Customer>();
        }


        public ICollection<Customer> GetByName(string name) =>
            _customerRepository.GetByName(name);

        public Customer GetCustomerWithAdressesAndContacts(Guid id) =>
            _customerRepository.GetCustomerWithAdressesAndContacts(id);

        public Customer GetFullCustomer(Guid id) =>
            _customerRepository.GetFullCustomer(id);
    }
}