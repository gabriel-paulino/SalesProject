using SalesProject.Api.ViewModels.Address;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace SalesProject.Api.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _uow;

        public AddressService(
            IAddressRepository addressRepository,
            IUnitOfWork uow)
        {
            _addressRepository = addressRepository;
            _uow = uow;
        }

        public Address Create(object createAddressViewModel)
        {
            var model = (CreateAddressViewModel)createAddressViewModel;

            var address =
                    new Address(
                        description: model.Description,
                        zipCode: model.ZipCode,
                        type: (AddressType)model.Type,
                        street: model.Street,
                        neighborhood: model.Neighborhood,
                        number: model.Number,
                        city: model.City,
                        state: model.State,
                        customerId: Guid.Parse(model.CustomerId));

            if (!address.Valid)
                return address;

            address.SetCodeCity(codeCity: model.CodeCity);

            _addressRepository.Create(address);
            _uow.Commit();

            return address;
        }

        public bool Delete(Guid id)
        {
            var address = _addressRepository.Get(id);

            if (address is null)
                return false;

            _addressRepository.Delete(address);
            _uow.Commit();

            return true;
        }

        public Address Edit(Guid id, object editAddressViewModel)
        {
            var model = (EditAddressViewModel)editAddressViewModel;
            var currentAddress = _addressRepository.Get(id);

            if (currentAddress is null)
                return null;

            var updatedAddress = currentAddress.
                        Edit(
                        description: model.Description,
                        zipCode: model.ZipCode,
                        type: (AddressType)model.Type,
                        street: model.Street,
                        neighborhood: model.Neighborhood,
                        number: model.Number,
                        city: model.City,
                        state: model.State);

            if (!updatedAddress.Valid)
                return updatedAddress;

            updatedAddress.SetCodeCity(codeCity: model.CodeCity);

            _addressRepository.Update(updatedAddress);
            _uow.Commit();

            return updatedAddress;
        }

        public Address Get(Guid id) =>
            _addressRepository.Get(id);

        public ICollection<Address> GetByCity(string city) =>
            _addressRepository.GetByCity(city);

        public ICollection<Address> GetByCustomerId(Guid customerId) =>
            _addressRepository.GetByCustomerId(customerId);

        public ICollection<Address> GetByDescription(string description) =>
            _addressRepository.GetByDescription(description);
    }
}