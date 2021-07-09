using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IAddressService
    {
        Address Get(Guid id);
        ICollection<Address> GetByDescription(string description);
        ICollection<Address> GetByCity(string city);
        ICollection<Address> GetByCustomerId(Guid customerId);
        Address Create(object createAddressViewModel);
        bool Delete(Guid id);
        Address Edit(Guid id, object editAddressViewModel);
    }
}