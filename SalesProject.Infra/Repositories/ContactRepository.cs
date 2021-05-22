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
        private readonly DataContext _context;
        private bool _disposed = false;

        public ContactRepository(DataContext context) =>
            _context = context;

        ~ContactRepository() =>
            Dispose();

        public void Create(Contact contact) =>
            _context.Contacts.Add(contact);

        public void Delete(Contact contact) =>
            _context.Contacts.Remove(contact);

        public Contact Get(Guid id) =>
            _context.Contacts.Find(id);

        public List<Contact> GetByName(string name) =>
            _context.Contacts.Where(x => x.FullName.Contains(name)).ToList();

        public List<Contact> GetByCustomerId(Guid customerId) =>
            _context.Contacts.Where(x => x.CustomerId.Equals(customerId)).ToList();

        public void Update(Contact contact) =>
            _context.Entry<Contact>(contact).State = EntityState.Modified;

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}