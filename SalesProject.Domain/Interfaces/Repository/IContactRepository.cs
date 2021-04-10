using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IContactRepository : IDisposable
    {
        List<Contact> GetAll();
        Contact Get(Guid id);
        List<Contact> GetByName(string name);
        void Create(Contact contact);
        void Update(Contact contact);
        void Delete(Contact contact);
    }
}