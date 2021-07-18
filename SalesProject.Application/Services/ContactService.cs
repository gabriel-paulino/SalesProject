using SalesProject.Application.ViewModels.Contact;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace SalesProject.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _uow;

        public ContactService(
            IContactRepository contactRepository,
            IUnitOfWork uow)
        {
            _contactRepository = contactRepository;
            _uow = uow;
        }

        public Contact Create(object createContactViewModel)
        {
            var model = (CreateContactViewModel)createContactViewModel;

            var contact =
                new Contact(
                    firstName: model.FirstName,
                    lastName: model.LastName,
                    email: model.Email,
                    whatsApp: model.WhatsApp,
                    phone: model.Phone,
                    customerId: Guid.Parse(model.CustomerId));

            if (!contact.Valid)
                return contact;

            _contactRepository.Create(contact);
            _uow.Commit();

            return contact;
        }

        public bool Delete(Guid id)
        {
            var address = _contactRepository.Get(id);

            if (address is null)
                return false;

            _contactRepository.Delete(address);
            _uow.Commit();

            return true;
        }

        public Contact Edit(Guid id, object editContactViewModel)
        {
            var model = (EditContactViewModel)editContactViewModel;
            var currentContact = _contactRepository.Get(id);

            if (currentContact is null)
                return null;

            var updatedContact = currentContact.
                        Edit(
                        firstName: model.FirstName,
                        lastName: model.LastName,
                        email: model.Email,
                        whatsApp: model.WhatsApp,
                        phone: model.Phone);

            if (!updatedContact.Valid)
                return updatedContact;

            _contactRepository.Update(updatedContact);
            _uow.Commit();

            return updatedContact;
        }

        public Contact Get(Guid id) =>
            _contactRepository.Get(id);

        public ICollection<Contact> GetByCustomerId(Guid customerId) =>
            _contactRepository.GetByCustomerId(customerId);

        public ICollection<Contact> GetByName(string name) =>
            _contactRepository.GetByName(name);
    }
}