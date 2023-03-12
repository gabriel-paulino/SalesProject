﻿using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetAsync(Guid id);
        Customer GetFullCustomer(Guid id);
        Customer GetCustomerWithAdressesAndContacts(Guid id);
        ICollection<Customer> GetByName(string name);
        Customer Create(object createCustomerViewModel, ref bool hasConflit);
        Customer CreateCustomerWithAdressesAndContacts(object createCompleteCustomerViewModel, ref bool hasConflit);
        bool Delete(Guid id);
        Customer Edit(Guid id, object editCustomerViewModel);
        Customer EditCustomerWithAdressesAndContacts(Guid id, object editCompleteCustomerViewModel);
    }
}