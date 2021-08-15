using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Tests.Constants;
using System;

namespace SalesProject.Domain.Tests.Entities
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void ShouldReturnSuccessWhenAddressIsValid()
        {
            var address = GetAddress();

            Assert.IsNotNull(address);
            Assert.IsTrue(address.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenDescriptionIsEmpty()
        {
            var addressWithInvalidDescription = GetAddress(description: string.Empty);

            Assert.IsNotNull(addressWithInvalidDescription);
            Assert.IsFalse(addressWithInvalidDescription.Valid);
            Assert.AreEqual(string.Empty, addressWithInvalidDescription.Description);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenZipCodeIsEmpty()
        {
            var addressWithInvalidZipCode = GetAddress(zipCode: string.Empty);

            Assert.IsNotNull(addressWithInvalidZipCode);
            Assert.IsFalse(addressWithInvalidZipCode.Valid);
            Assert.AreEqual(string.Empty, addressWithInvalidZipCode.ZipCode);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenZipCodeIsInvalid()
        {
            string invalidZipCode = "invalidValue";
            var addressWithInvalidZipCode = GetAddress(zipCode: invalidZipCode);

            Assert.IsNotNull(addressWithInvalidZipCode);
            Assert.IsFalse(addressWithInvalidZipCode.Valid);
            Assert.AreEqual(invalidZipCode, addressWithInvalidZipCode.ZipCode);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenTypeIsNull()
        {
            var addressWithInvalidType = GetAddress(type: null);

            Assert.IsNotNull(addressWithInvalidType);
            Assert.IsFalse(addressWithInvalidType.Valid);
            Assert.AreEqual(null, addressWithInvalidType.Type);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenStreetIsEmpty()
        {
            var addressWithInvalidStreet = GetAddress(street: string.Empty);

            Assert.IsNotNull(addressWithInvalidStreet);
            Assert.IsFalse(addressWithInvalidStreet.Valid);
            Assert.AreEqual(string.Empty, addressWithInvalidStreet.Street);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNeighborhoodIsEmpty()
        {
            var addressWithInvalidNeighborhood = GetAddress(neighborhood: string.Empty);

            Assert.IsNotNull(addressWithInvalidNeighborhood);
            Assert.IsFalse(addressWithInvalidNeighborhood.Valid);
            Assert.AreEqual(string.Empty, addressWithInvalidNeighborhood.Neighborhood);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNumberIsZero()
        {
            var addressWithNumberZero = GetAddress(number: 0);

            Assert.IsNotNull(addressWithNumberZero);
            Assert.IsFalse(addressWithNumberZero.Valid);
            Assert.AreEqual(0, addressWithNumberZero.Number);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNumberLessThanZero()
        {
            int invalidNumber = -50;
            var addressWithInvalidNumber = GetAddress(number: invalidNumber);

            Assert.IsNotNull(addressWithInvalidNumber);
            Assert.IsFalse(addressWithInvalidNumber.Valid);
            Assert.AreEqual(invalidNumber, addressWithInvalidNumber.Number);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCityIsEmpty()
        {
            var addressWithInvalidCity = GetAddress(city: string.Empty);

            Assert.IsNotNull(addressWithInvalidCity);
            Assert.IsFalse(addressWithInvalidCity.Valid);
            Assert.AreEqual(string.Empty, addressWithInvalidCity.City);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenStateIsEmpty()
        {
            var addressWithInvalidState = GetAddress(state: string.Empty);

            Assert.IsNotNull(addressWithInvalidState);
            Assert.IsFalse(addressWithInvalidState.Valid);
            Assert.AreEqual(string.Empty, addressWithInvalidState.State);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenSetAddressWithAnValidCodeCity()
        {
            var address = GetAddress();
            string codeIbgeSaoPaulo = "3550308";

            address.SetCodeCity(codeIbgeSaoPaulo);

            Assert.IsNotNull(address.CodeCity);
            Assert.IsTrue(address.Valid);
            Assert.AreEqual(codeIbgeSaoPaulo, address.CodeCity);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenEditAddress()
        {
            var address = GetAddress();

            string updatedDescription = "Edited Address";
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

        public static Address GetAddress(
            string description = AddressTestsConstants.ValidDescription,
            string zipCode = AddressTestsConstants.ValidZipCode,
            AddressType? type = AddressTestsConstants.ValidType,
            string street = AddressTestsConstants.ValidStreet,
            string neighborhood = AddressTestsConstants.ValidNeighborhood,
            int number = AddressTestsConstants.ValidNumber,
            string city = AddressTestsConstants.ValidCity,
            string state = AddressTestsConstants.ValidState)
            => new Address(
                description: description,
                zipCode: zipCode,
                type: type,
                street: street,
                neighborhood: neighborhood,
                number: number,
                city: city,
                state: state,
                customerId: Guid.NewGuid());
    }
}