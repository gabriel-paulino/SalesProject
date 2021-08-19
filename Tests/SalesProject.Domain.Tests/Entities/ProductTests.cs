using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Tests.Constants;
using System;

namespace SalesProject.Domain.Tests.Entities
{
    [TestClass]
    public class ProductTests
    {
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