using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IContactRepository : IDisposable
    {
        Contact Get(Guid id);
        List<Contact> GetByName(string name);
        List<Contact> GetByCustomerId(Guid customerId);
        void Create(Contact contact);
        void Update(Contact contact);
        void Delete(Contact contact);
    }
}