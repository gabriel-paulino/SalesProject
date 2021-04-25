using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _dataContext;
        private bool _disposed = false;

        public ContactRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        ~ContactRepository() =>
            Dispose();

        public void Create(Contact contact) =>
            _dataContext.Contacts.Add(contact);

        public void Delete(Contact contact) =>
            _dataContext.Contacts.Remove(contact);

        public Contact Get(Guid id) =>
            _dataContext.Contacts.Find(id);

        public List<Contact> GetByName(string name) =>
            _dataContext.Contacts.Where(x => x.FullName.Contains(name)).ToList();

        public List<Contact> GetByCustomerId(Guid customerId) =>
            _dataContext.Contacts.Where(x => x.CustomerId.Equals(customerId)).ToList();

        public void Update(Contact contact) =>
            _dataContext.Entry<Contact>(contact).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}