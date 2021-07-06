using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IAddressService
    {
        Address Get(Guid id);
        List<Address> GetByDescription(string description);
        List<Address> GetByCity(string city);
        List<Address> GetByCustomerId(Guid customerId);
        Address Create(object createAddressViewModel);
        bool Delete(Guid id);
        Address Edit(Guid id, object editAddressViewModel);
    }
}