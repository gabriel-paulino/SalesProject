using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Tests.Constants;
using System;

namespace SalesProject.Domain.Tests.Entities
{
    [TestClass]
    public class ContactTests
    {
        [TestMethod]
        public void ShouldReturnSuccessWhenContactIsValid()
        {
            var contact = GetContact();

            Assert.IsNotNull(contact);
            Assert.IsTrue(contact.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenFirstNameIsEmpty()
        {
            var contactWithEmptyFirstName = GetContact(firstName: string.Empty);

            Assert.IsNotNull(contactWithEmptyFirstName);
            Assert.IsFalse(contactWithEmptyFirstName.Valid);
            Assert.AreEqual(string.Empty, contactWithEmptyFirstName.FirstName);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenEmailIsEmpty()
        {
            var contactWithEmptyEmail = GetContact(email: string.Empty);

            Assert.IsNotNull(contactWithEmptyEmail);
            Assert.IsFalse(contactWithEmptyEmail.Valid);
            Assert.AreEqual(string.Empty, contactWithEmptyEmail.Email);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenPhoneIsEmpty()
        {
            var contactWithEmptyPhone = GetContact(phone: string.Empty);

            Assert.IsNotNull(contactWithEmptyPhone);
            Assert.IsFalse(contactWithEmptyPhone.Valid);
            Assert.AreEqual(string.Empty, contactWithEmptyPhone.Phone);
        }

        [TestMethod]
        public void ShouldReturnFormattedFullNameWhenFirstNameAndLastNameWasInformed()
        {
            var contact = GetContact(firstName: "Bruce", lastName: "Wayne");

            Assert.AreEqual("Wayne, Bruce", contact.FullName);
        }

        [TestMethod]
        public void ShouldReturnOnlyFirstNameWhenLastNameWasNotInformed()
        {
            var contact = GetContact(firstName: "Bruce", lastName: string.Empty);

            Assert.AreEqual("Bruce", contact.FullName);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenEditContact()
        {
            var contact = GetContact();

            string updatedFirstName = "Tony";
            string updatedLastName = "Stark";
            string updatedEmail = "tony@stark.industries.com";
            string updatedWhatsApp = "(11) 2721-6965";
            string updatedPhone = "(11) 2721-6965";

            contact.Edit(
                updatedFirstName,
                updatedLastName,
                updatedEmail,
                updatedWhatsApp,
                updatedPhone);

            Assert.AreEqual(updatedFirstName, contact.FirstName);
            Assert.AreEqual(updatedLastName, contact.LastName);
            Assert.AreEqual(updatedEmail, contact.Email);
            Assert.AreEqual(updatedWhatsApp, contact.WhatsApp);
            Assert.AreEqual(updatedPhone, contact.Phone);
            Assert.AreEqual($"{updatedLastName}, {updatedFirstName}", contact.FullName);
            Assert.IsTrue(contact.Valid);
        }

        public static Contact GetContact(
            string firstName = ContactTestsConstants.ValidFirstName,
            string lastName = ContactTestsConstants.ValidLastName,
            string email = ContactTestsConstants.ValidEmail,
            string whatsApp = ContactTestsConstants.ValidWhatsApp,
            string phone = ContactTestsConstants.ValidPhone)
            => new Contact(
                firstName: firstName,
                lastName: lastName,
                email: email,
                whatsApp: whatsApp,
                phone: phone,
                customerId: Guid.NewGuid());
    }
}