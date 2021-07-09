using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using System;

namespace SalesProject.Tests.Entities
{
    [TestClass]
    public class AddressTests
    {
        private readonly string _description;
        private readonly string _zipCode;
        private readonly AddressType _type;
        private readonly string _street;
        private readonly string _neighborhood;
        private readonly int _number;
        private readonly string _city;
        private readonly string _state;
        private readonly Guid _customerId;
        private readonly string _codeCity;

        public AddressTests()
        {
            _description = "Balcão 1";
            _zipCode = "02248-050";
            _type = AddressType.Other;
            _street = "Rua Francisca Maria de Souza";
            _neighborhood = "Parada Inglesa";
            _number = 131;
            _city = "São Paulo";
            _state = "SP";
            _customerId = Guid.NewGuid();
            _codeCity = "3550308";
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddressIsValid()
        {
            var address = GetValidAddress();

            Assert.IsNotNull(address);
            Assert.IsTrue(address.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenDescriptionIsEmpty()
        {
            var invalidAddress = GetAddressWithAnInvalidAttribute("description");

            Assert.IsNotNull(invalidAddress);
            Assert.IsFalse(invalidAddress.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenZipCodeIsEmpty()
        {
            var invalidAddress = GetAddressWithAnInvalidAttribute("zipCode");

            Assert.IsNotNull(invalidAddress);
            Assert.IsFalse(invalidAddress.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenTypeIsNull()
        {
            var invalidAddress = GetAddressWithAnInvalidAttribute("type");

            Assert.IsNotNull(invalidAddress);
            Assert.IsFalse(invalidAddress.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenStreetIsEmpty()
        {
            var invalidAddress = GetAddressWithAnInvalidAttribute("street");

            Assert.IsNotNull(invalidAddress);
            Assert.IsFalse(invalidAddress.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNeighborhoodIsEmpty()
        {
            var invalidAddress = GetAddressWithAnInvalidAttribute("neighborhood");

            Assert.IsNotNull(invalidAddress);
            Assert.IsFalse(invalidAddress.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNumberIsZero()
        {
            var invalidAddress = GetAddressWithAnInvalidAttribute("number");

            Assert.IsNotNull(invalidAddress);
            Assert.IsFalse(invalidAddress.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCityIsEmpty()
        {
            var invalidAddress = GetAddressWithAnInvalidAttribute("city");

            Assert.IsNotNull(invalidAddress);
            Assert.IsFalse(invalidAddress.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenStateIsEmpty()
        {
            var invalidAddress = GetAddressWithAnInvalidAttribute("state");

            Assert.IsNotNull(invalidAddress);
            Assert.IsFalse(invalidAddress.Valid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenSetAddressWithAnValidCodeCity()
        {
            var address = GetValidAddress();

            address.SetCodeCity(_codeCity);

            Assert.IsNotNull(address.CodeCity);
            Assert.IsTrue(address.Valid);
            Assert.AreEqual(address.CodeCity, _codeCity);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenEditAddress()
        {
            var address = GetValidAddress();

            string updatedDescription = $"Edited Address";
            string updatedZipCode = "12237-900";
            AddressType? updatedType = AddressType.Billing;
            string updatedStreet = $"Rodovia Presidente Dutra km 156";
            string updatedNeighborhood = "Palmeiras de São José";
            int updatedNumber = 310;
            string updatedCity = "São José dos Campos";
            string updatedState = "SP";

            address.Edit(
                updatedDescription, 
                updatedZipCode, 
                updatedType, 
                updatedStreet, 
                updatedNeighborhood, 
                updatedNumber, 
                updatedCity, 
                updatedState);
            
            Assert.AreEqual(address.Description, updatedDescription);
            Assert.AreEqual(address.ZipCode, updatedZipCode);
            Assert.AreEqual(address.Type, updatedType);
            Assert.AreEqual(address.Street, updatedStreet);
            Assert.AreEqual(address.Neighborhood, updatedNeighborhood);
            Assert.AreEqual(address.Number, updatedNumber);
            Assert.AreEqual(address.City, updatedCity);
            Assert.AreEqual(address.State, updatedState);
            Assert.IsTrue(address.Valid);
        }

        private Address GetAddressWithAnInvalidAttribute(string invalidAttribute) =>
             new Address(
                description: invalidAttribute == "description" ? string.Empty : _description,
                zipCode: invalidAttribute == "zipCode" ? string.Empty : _zipCode,
                type: invalidAttribute == "type" ? null : _type,
                street: invalidAttribute == "street" ? string.Empty : _street,
                neighborhood: invalidAttribute == "neighborhood" ? string.Empty : _neighborhood,
                number: invalidAttribute == "number" ? 0 : _number,
                city: invalidAttribute == "city" ? string.Empty : _city,
                state: invalidAttribute == "state" ? string.Empty : _state,
                customerId: _customerId);

        private Address GetValidAddress() =>
             new Address(
                description: _description,
                zipCode: _zipCode,
                type: _type,
                street: _street,
                neighborhood: _neighborhood,
                number: _number,
                city: _city,
                state: _state,
                customerId: _customerId);
    }
}