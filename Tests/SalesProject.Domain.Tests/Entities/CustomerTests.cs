using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Tests.Constants;
using System;

namespace SalesProject.Domain.Tests.Entities
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void ShouldReturnSuccessWhenCustomerIsValid()
        {
            var customer = GetCustomer();

            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.Valid);
        }

        [TestMethod]
        public void ShouldAddAnAddress()
        {
            var customer = GetCustomer();
            var address = AddressTests.GetAddress();

            customer.AddAddress(address);

            Assert.AreEqual(1, customer.Adresses.Count);
        }

        [TestMethod]
        public void ShouldRemoveAnAddress()
        {
            var customer = GetCustomer();
            var address = AddressTests.GetAddress();
            customer.AddAddress(address);

            customer.RemoveAddress(address);

            Assert.AreEqual(0, customer.Adresses.Count);
        }

        [TestMethod]
        public void ShouldAddAContact()
        {
            var customer = GetCustomer();
            var contact = ContactTests.GetContact();

            customer.AddContact(contact);

            Assert.AreEqual(1, customer.Contacts.Count);
        }

        [TestMethod]
        public void ShouldRemoveAContact()
        {
            var customer = GetCustomer();
            var contact = ContactTests.GetContact();
            customer.AddContact(contact);

            customer.RemoveContact(contact);

            Assert.AreEqual(0, customer.Contacts.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCnpjIsEmpty()
        {
            var customerWithEmptyCnpj = GetCustomer(cnpj: string.Empty);

            Assert.IsNotNull(customerWithEmptyCnpj);
            Assert.IsFalse(customerWithEmptyCnpj.Valid);
            Assert.AreEqual(string.Empty, customerWithEmptyCnpj.Cnpj);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCnpjIsInvalid()
        {
            string invalidCnpj = "ThisCnpjIsInvalid";
            var customerWithInvalidCnpj = GetCustomer(cnpj: invalidCnpj);

            Assert.IsNotNull(customerWithInvalidCnpj);
            Assert.IsFalse(customerWithInvalidCnpj.Valid);
            Assert.AreEqual(invalidCnpj, customerWithInvalidCnpj.Cnpj);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCompanyNameIsEmpty()
        {
            var customerWithEmptyCompanyName = GetCustomer(companyName: string.Empty);

            Assert.IsNotNull(customerWithEmptyCompanyName);
            Assert.IsFalse(customerWithEmptyCompanyName.Valid);
            Assert.AreEqual(string.Empty, customerWithEmptyCompanyName.CompanyName);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenStateRegistrationIsEmpty()
        {
            var customerWithEmptyStateRegistration = GetCustomer(stateRegistration: string.Empty);

            Assert.IsNotNull(customerWithEmptyStateRegistration);
            Assert.IsFalse(customerWithEmptyStateRegistration.Valid);
            Assert.AreEqual(string.Empty, customerWithEmptyStateRegistration.StateRegistration);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenEmailIsEmpty()
        {
            var customerWithEmptyEmail = GetCustomer(email: string.Empty);

            Assert.IsNotNull(customerWithEmptyEmail);
            Assert.IsFalse(customerWithEmptyEmail.Valid);
            Assert.AreEqual(string.Empty, customerWithEmptyEmail.Email);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenOpeningDateIsBiggerThanToday()
        {
            var tomorrow = DateTime.Now.AddDays(1);
            var customerWithInvalidOpening = GetCustomer(opening: tomorrow);

            Assert.IsFalse(customerWithInvalidOpening.Valid);
            Assert.AreEqual(tomorrow, customerWithInvalidOpening.Opening);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenEditCustomer()
        {
            var customer = GetCustomer();

            string updatedPhone = "(11) 2503-6002";
            string updatedMunicipalRegistration = "87654321";
            string updatedStateRegistration = "00879808000190";
            string updatedEmail = "ei.contabilidade2@gmail.com";

            customer.Edit(
                updatedPhone,
                updatedMunicipalRegistration,
                updatedStateRegistration,
                updatedEmail);

            Assert.AreEqual(updatedPhone, customer.Phone);
            Assert.AreEqual(updatedMunicipalRegistration, customer.MunicipalRegistration);
            Assert.AreEqual(updatedStateRegistration, customer.StateRegistration);
            Assert.AreEqual(updatedEmail, customer.Email);
            Assert.IsTrue(customer.Valid);
        }

        public static Customer GetCustomer(
            string cnpj = CustomerTestsConstants.ValidCnpj,
            string companyName = CustomerTestsConstants.ValidCompanyName,
            DateTime? opening = default(DateTime?),
            string phone = CustomerTestsConstants.ValidPhone,
            string municipalRegistration = CustomerTestsConstants.ValidMunicipalRegistration,
            string stateRegistration = CustomerTestsConstants.ValidStateRegistration,
            string email = CustomerTestsConstants.ValidEmail)
            => new Customer(
                cnpj: cnpj,
                companyName: companyName,
                opening: opening ?? new(year: 1900, month: 9, day: 3),
                phone: phone,
                municipalRegistration: municipalRegistration,
                stateRegistration: stateRegistration,
                email: email);

    }
}