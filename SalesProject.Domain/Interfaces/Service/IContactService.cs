using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IContactService
    {
        Contact Get(Guid id);
        ICollection<Contact> GetByName(string name);
        ICollection<Contact> GetByCustomerId(Guid customerId);
        Contact Create(object createContactViewModel);
        bool Delete(Guid id);
        Contact Edit(Guid id, object editContactViewModel);
    }
}