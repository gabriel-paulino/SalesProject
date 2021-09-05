using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Tests.Constants;
using System;

namespace SalesProject.Domain.Tests.Entities
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ShouldReturnSuccessWhenProductIsValid()
        {
            Product product = new(
                name: "LEITE",
                ncmCode: "04",
                combinedPrice: 1.85m,
                additionalCosts: 0.35m,
                combinedQuantity: 5000,
                details: "",
                customerId: Guid.NewGuid());

            Assert.IsTrue(product.Valid);
            Assert.AreEqual(0, product.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenProductNameIsEmpty()
        {
            Product unnamedProduct = new(
                name: string.Empty,
                ncmCode: "84804910",
                combinedPrice: 0.85m,
                additionalCosts: 0.15m,
                combinedQuantity: 50000,
                details: "BANHO DE ZINCO",
                customerId: Guid.NewGuid());

            Assert.IsFalse(unnamedProduct.Valid);
            Assert.AreEqual(string.Empty, unnamedProduct.Name);
            Assert.AreEqual(1, unnamedProduct.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNcmCodeIsEmpty()
        {
            Product productInvalidNcmCode = new(
                name: "COQUILHA OVAL 1,5",
                ncmCode: string.Empty,
                combinedPrice: 0.85m,
                additionalCosts: 0.15m,
                combinedQuantity: 50000,
                details: "BANHO DE ZINCO",
                customerId: Guid.NewGuid());

            Assert.IsFalse(productInvalidNcmCode.Valid);
            Assert.AreEqual(string.Empty, productInvalidNcmCode.NcmCode);
        }

        [TestMethod]
        [DataRow("123")]
        [DataRow("0a")]
        public void ShouldReturnErrorWhenNcmCodeIsEmpty(string invalidNcmCode)
        {
            Product productInvalidNcmCode = new(
                name: "COQUILHA OVAL 1,5",
                ncmCode: invalidNcmCode,
                combinedPrice: 0.85m,
                additionalCosts: 0.15m,
                combinedQuantity: 50000,
                details: "BANHO DE ZINCO",
                customerId: Guid.NewGuid());

            Assert.IsFalse(productInvalidNcmCode.Valid);
            Assert.AreEqual(invalidNcmCode, productInvalidNcmCode.NcmCode);
            Assert.AreEqual(1, productInvalidNcmCode.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCombinedPriceIsZero()
        {
            Product productWithInvalidCombinedPrice = new(
                name: "COQUILHA OVAL 1,5",
                ncmCode: "84804910",
                combinedPrice: 0,
                additionalCosts: 0.15m,
                combinedQuantity: 50000,
                details: "BANHO DE ZINCO",
                customerId: Guid.NewGuid());

            Assert.IsFalse(productWithInvalidCombinedPrice.Valid);
            Assert.AreEqual(0, productWithInvalidCombinedPrice.CombinedPrice);
            Assert.AreEqual(1, productWithInvalidCombinedPrice.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCombinedPriceIsNegative()
        {
            Product productWithInvalidCombinedPrice = new(
                name: "COQUILHA OVAL 1,5",
                ncmCode: "84804910",
                combinedPrice: -0.05m,
                additionalCosts: 0.15m,
                combinedQuantity: 50000,
                details: "BANHO DE ZINCO",
                customerId: Guid.NewGuid());

            Assert.IsFalse(productWithInvalidCombinedPrice.Valid);
            Assert.AreEqual(-0.05m, productWithInvalidCombinedPrice.CombinedPrice);
            Assert.AreEqual(1, productWithInvalidCombinedPrice.Notifications.Count);
        }

        [DataRow(-5000)]
        [DataRow(0)]
        [TestMethod]
        public void ShouldReturnErrorWhenCombinedQuantityIsInvalid(int invalidQuantity)
        {
            Product productWithInvalidCombinedPrice = new(
                name: "COQUILHA OVAL 1,5",
                ncmCode: "84804910",
                combinedPrice: 0.85m,
                additionalCosts: 0.15m,
                combinedQuantity: invalidQuantity,
                details: "BANHO DE ZINCO",
                customerId: Guid.NewGuid());

            Assert.IsFalse(productWithInvalidCombinedPrice.Valid);
            Assert.AreEqual(invalidQuantity, productWithInvalidCombinedPrice.CombinedQuantity);
            Assert.AreEqual(1, productWithInvalidCombinedPrice.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenAdditionalCostsIsNegative()
        {
            decimal invalidAdditionalCosts = -0.02m;

            Product productWithInvalidCombinedPrice = new(
                name: "COQUILHA OVAL 1,5",
                ncmCode: "84804910",
                combinedPrice: 0.85m,
                additionalCosts: invalidAdditionalCosts,
                combinedQuantity: 50000,
                details: "BANHO DE ZINCO",
                customerId: Guid.NewGuid());

            Assert.IsFalse(productWithInvalidCombinedPrice.Valid);
            Assert.AreEqual(invalidAdditionalCosts, productWithInvalidCombinedPrice.AdditionalCosts);
            Assert.AreEqual(1, productWithInvalidCombinedPrice.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenEditProductWithCorrectInputsData()
        {
            var product = GetProduct();

            string updatedName = "COQUILHA REDONDA 2";
            string updatedNcmCode = "84804912";            
            decimal updatedCombinedPrice = 0.85m;
            decimal updatedAdditionalCosts = 0;
            int updatedCombinedQuantity = 30000;
            string updatedDetails = "BANHO DE ZINCO";

            product.Edit(
                name: updatedName,
                ncmCode: updatedNcmCode,
                combinedPrice: updatedCombinedPrice,
                additionalCosts: updatedAdditionalCosts,
                combinedQuantity: updatedCombinedQuantity,
                details: updatedDetails);

            Assert.IsTrue(product.Valid);
            Assert.AreEqual(updatedName, product.Name);
            Assert.AreEqual(updatedNcmCode, product.NcmCode);
            Assert.AreEqual(updatedCombinedPrice, product.CombinedPrice);
            Assert.AreEqual(updatedAdditionalCosts, product.AdditionalCosts);
            Assert.AreEqual(updatedCombinedQuantity, product.CombinedQuantity);
            Assert.AreEqual(updatedDetails, product.Details);
        }

        public static Product GetProduct(
            string name = ProductTestsConstants.ValidName,
            string ncmCode = ProductTestsConstants.ValidNcmCode,
            decimal combinedPrice = ProductTestsConstants.ValidCombinedPrice,
            decimal additionalCosts = ProductTestsConstants.ValidAdditionalCosts,
            int combinedQuantity = ProductTestsConstants.ValidCombinedQuantity,
            string details = ProductTestsConstants.ValidDetails)
            => new Product(
                name: name,
                ncmCode: ncmCode,
                combinedPrice: combinedPrice,
                additionalCosts: additionalCosts,
                combinedQuantity: combinedQuantity,
                details: details,
                customerId: Guid.NewGuid());
    }
}