using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Infra.Context;
using SalesProject.Infra.Repositories;
using SalesProject.Infra.UoW;
using System.Linq;

namespace SalesProject.Infra.Tests.Repositories
{
    [TestClass]
    public class AddressRepositoryTests
    {
        private readonly DataContext _fakeContext;
        private readonly UnitOfWork _uow;
        private readonly AddressRepository _addressRepository;
        
        public AddressRepositoryTests()
        {
            var dbInMemory = new DBInMemory();
            _fakeContext = dbInMemory.GetContext();

            _uow = new UnitOfWork(_fakeContext);
            _addressRepository = new AddressRepository(_fakeContext);            
        }

        [TestMethod]
        public void ShouldReturnTwoAdressesWhenGetByCityInFakeDataBase()
        {
            var adresses = _addressRepository.GetByCity("São Paulo");

            Assert.AreEqual(2, adresses.Count);
        }

        [TestMethod]
        public void ShouldReturnThreeAdressesWhenGetByCustomerIdInFakeDataBase()
        {
            var customerId = _fakeContext.Customers.FirstOrDefault().Id;
            var adresses = _addressRepository.GetByCustomerId(customerId);

            Assert.AreEqual(3, adresses.Count);
        }

        [TestMethod]
        public void ShouldReturnOneAdressesWhenGetByDescriptionIdInFakeDataBase()
        {
            var adresses = _addressRepository.GetByDescription("Escritório");
            var address = adresses.FirstOrDefault();

            Assert.AreEqual(1, adresses.Count);
            Assert.AreEqual("02123044", address.ZipCode);
            Assert.AreEqual(AddressType.Billing, address.Type);
            Assert.AreEqual("Travessa Gaspar Raposo", address.Street);
            Assert.AreEqual("Jardim Japão", address.Neighborhood);
            Assert.AreEqual("São Paulo", address.City);
            Assert.AreEqual("SP", address.State);
        }

        [TestMethod]
        public void ShouldReturnOneAddressWhenGetByIdInFakeDataBase()
        {
            var addressId = _fakeContext.Addreses.FirstOrDefault().Id;
            var address = _addressRepository.Get(addressId);

            Assert.AreEqual("02123044", address.ZipCode);
            Assert.AreEqual(AddressType.Billing, address.Type);
            Assert.AreEqual("Travessa Gaspar Raposo", address.Street);
            Assert.AreEqual("Jardim Japão", address.Neighborhood);
            Assert.AreEqual("São Paulo", address.City);
            Assert.AreEqual("SP", address.State);
        }

        [TestMethod]
        public void ShouldAddOneAddressOnFakeDataBaseWhenCreate()
        {
            var address = new Address(
                description: "Balcão Americana",
                zipCode: "18046-430",
                type: AddressType.Other,
                street: "Rua Eduardo Milani",
                neighborhood: "Vila Bela",
                number: 166,
                city: "Americana",
                state: "SP",
                customerId: _fakeContext.Customers.FirstOrDefault().Id);

            address.SetCodeCity("3501608");

            _addressRepository.Create(address);
            _uow.Commit();

            Assert.AreEqual(4, _fakeContext.Addreses.Count());
        }

        [TestMethod]
        public void ShouldUpdateAddressOnFakeDataBaseWhenChangeNumber()
        {
            var addressId = _fakeContext.Addreses.FirstOrDefault().Id;
            var address = _addressRepository.Get(addressId);

            int updatedNumber = address.Number + 100;

            address.Edit(
                description: address.Description,
                zipCode: address.ZipCode,
                type: address.Type,
                street: address.Street,
                neighborhood: address.Neighborhood,
                number: updatedNumber,
                city: address.City,
                state: address.State);

            _addressRepository.Update(address);
            _uow.Commit();

            Assert.AreEqual(400, _addressRepository.Get(addressId).Number);
        }

        [TestMethod]
        public void ShouldUpdateAddressOnFakeDataBaseWhenAllValues()
        {
            var addressId = _fakeContext.Addreses.FirstOrDefault().Id;
            var currentAddress = _addressRepository.Get(addressId);

            currentAddress.Edit(
                description: "Balcão Americana",
                zipCode: "14406-574",
                type: AddressType.Other,
                street: "Rua Santa Izabel",
                neighborhood: "Parque Residencial Santa Maria",
                number: 148,
                city: "Franca",
                state: "SP");

            _addressRepository.Update(currentAddress);
            _uow.Commit();

            var updatedAddress = _addressRepository.Get(addressId);

            Assert.AreEqual("14406-574", updatedAddress.ZipCode);
            Assert.AreEqual(AddressType.Other, updatedAddress.Type);
            Assert.AreEqual("Rua Santa Izabel", updatedAddress.Street);
            Assert.AreEqual("Parque Residencial Santa Maria", updatedAddress.Neighborhood);
            Assert.AreEqual(148, updatedAddress.Number);
            Assert.AreEqual("Franca", updatedAddress.City);
            Assert.AreEqual("SP", updatedAddress.State);
        }

        [TestMethod]
        public void ShouldDeleteAddressOnFakeDataBaseWhenGiveId()
        {
            var addressId = _fakeContext.Addreses.FirstOrDefault().Id;
            var address = _addressRepository.Get(addressId);

            _addressRepository.Delete(address);
            _uow.Commit();

            Assert.AreEqual(2, _fakeContext.Addreses.Count());
        }
    }
}