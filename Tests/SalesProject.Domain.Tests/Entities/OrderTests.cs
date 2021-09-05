using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using System;

namespace SalesProject.Domain.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void ShouldReturnSuccessWhenOrderIsValid()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This sales order has no items, but may include items in the future",
                customer: CustomerTests.GetCustomer());

            Assert.IsNotNull(order);
            Assert.IsTrue(order.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCustomerIsNull()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order orderWithoutCustomer = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This order didn't have an customer",
                customer: null);

            Assert.IsFalse(orderWithoutCustomer.Valid);
            Assert.AreEqual(null, orderWithoutCustomer.Customer);
            Assert.AreEqual(1, orderWithoutCustomer.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenPostingDateIsBiggerThanDeliveryDate()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order orderWithIncorrrectDeliveryDate = new(
                postingDate: nextMonth,
                deliveryDate: DateTime.Today,
                observation: "This order has an incorrect Delivery date",
                customer: CustomerTests.GetCustomer());

            Assert.IsFalse(orderWithIncorrrectDeliveryDate.Valid);
            Assert.IsFalse(orderWithIncorrrectDeliveryDate.PostingDate <= orderWithIncorrrectDeliveryDate.DeliveryDate);
            Assert.AreEqual(1, orderWithIncorrrectDeliveryDate.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenTryApproveAnOrderWithoutLines()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This order has Open status",
                customer: CustomerTests.GetCustomer());

            order.Approve();

            Assert.IsFalse(order.Valid);
            Assert.AreEqual(OrderStatus.Open, order.Status);
            Assert.AreEqual(1, order.Notifications.Count);
        }

        [TestMethod]
        public void ShouldApproveWhenOrderHasLines()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This order has an order item",
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);

            order.Approve();

            Assert.IsTrue(order.Valid);
            Assert.AreEqual(OrderStatus.Approved, order.Status);
            Assert.AreEqual(0, order.Notifications.Count);
        }

        [TestMethod]
        public void ShouldCalculateTotalOrderWhenAddAnLines()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This order has an order item",
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);

            decimal expected = product.CombinedQuantity * (product.CombinedPrice + product.AdditionalCosts);

            Assert.IsTrue(order.Valid);
            Assert.AreEqual(expected, order.TotalOrder);
            Assert.AreEqual(0, order.Notifications.Count);
        }

        [DataRow(5)]
        [DataRow(3)]
        [DataRow(4)]
        [TestMethod]
        public void ShouldCalculateTotalOrderWhenAddSomeLines(int quantityOrderItem)
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This order has some order item",
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            for (int i = 0; i < quantityOrderItem; i++)
                order.AddOrderLine(orderLine);

            decimal expected = quantityOrderItem * (product.CombinedQuantity * (product.CombinedPrice + product.AdditionalCosts));

            Assert.IsTrue(order.Valid);
            Assert.AreEqual(expected, order.TotalOrder);
            Assert.AreEqual(0, order.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenTryBillAnOrderWithOpenStatus()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This order has Open status",
                customer: CustomerTests.GetCustomer());

            order.Bill();

            Assert.IsFalse(order.Valid);
            Assert.AreEqual(OrderStatus.Open, order.Status);
            Assert.AreEqual(1, order.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenTryApproveAnOrderWithCanceledStatus()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
               postingDate: DateTime.Today,
               deliveryDate: nextMonth,
               observation: "Canceled Order",
               customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);
            order.Cancel();

            order.Approve();

            Assert.IsFalse(order.Valid);
            Assert.AreEqual(OrderStatus.Canceled, order.Status);
            Assert.AreEqual(1, order.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenTryCancelAnOrderWithApprovedStatus()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
               postingDate: DateTime.Today,
               deliveryDate: nextMonth,
               observation: "Canceled Order",
               customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);

            order.Approve();
            order.Cancel();

            Assert.IsFalse(order.Valid);
            Assert.AreEqual(OrderStatus.Approved, order.Status);
            Assert.AreEqual(1, order.Notifications.Count);
        }

        [TestMethod]
        public void ShouldBillAnOrderWhenOrderHasAtLeastALineAndWasApproved()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This order will be Billed",
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);
            order.Approve();

            order.Bill();

            Assert.IsTrue(order.Valid);
            Assert.AreEqual(OrderStatus.Billed, order.Status);
            Assert.AreEqual(0, order.Notifications.Count);
        }

        [TestMethod]
        public void ShouldBeAbleToBillWhenOrderHasAtLeastALineAndWasApproved()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This sales order can be invoiced",
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);
            order.Approve();

            bool expected = true;
            bool actual = order.CanBillThisOrder();

            Assert.IsTrue(order.Valid);
            Assert.AreEqual(OrderStatus.Approved, order.Status);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldNotBeAbleToBillWhenOrderHasAnLineAndWasNotApproved()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This sales order can be invoiced",
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);

            bool expected = false;
            bool actual = order.CanBillThisOrder();

            Assert.IsTrue(order.Valid);
            Assert.AreEqual(OrderStatus.Open, order.Status);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenEditOrderWithCorrectInputsData()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: string.Empty,
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);

            DateTime updatedPostingDate = DateTime.Today.AddDays(7);
            DateTime updatedDeliveryDate = updatedPostingDate.AddMonths(1);
            string updatedObservation = "Sorry, this sales order should be created next week.";


            order.Edit(
                postingDate: updatedPostingDate,
                deliveryDate: updatedDeliveryDate,
                observation: updatedObservation);

            Assert.AreEqual(order.PostingDate, updatedPostingDate);
            Assert.AreEqual(order.DeliveryDate, updatedDeliveryDate);
            Assert.AreEqual(order.Observation, updatedObservation);
            Assert.IsTrue(order.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenEditOrderWithPostingDateBiggerThanDeliveryDate()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: string.Empty,
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);

            DateTime updatedDeliveryDate = DateTime.Today.AddDays(7);
            DateTime updatedPostingDate = updatedDeliveryDate.AddMonths(1);
            string updatedObservation = "This order has Posting Date bigger than Delivery Date. When Edited";

            order.Edit(
                postingDate: updatedPostingDate,
                deliveryDate: updatedDeliveryDate,
                observation: updatedObservation);

            Assert.IsFalse(order.Valid);
            Assert.AreEqual(1, order.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenRemoveAllOrderLines()
        {
            var nextMonth = DateTime.Today.AddMonths(1);

            Order order = new(
                postingDate: DateTime.Today,
                deliveryDate: nextMonth,
                observation: "This sales order has no items",
                customer: CustomerTests.GetCustomer());

            var product = ProductTests.GetProduct();

            var orderLine = new OrderLine(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            order.AddOrderLine(orderLine);

            order.RemoveOrderLine(orderLine);

            Assert.IsTrue(order.Valid);
            Assert.AreEqual(0, order.OrderLines.Count);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void ShouldReturnErrorWhenOrderLineHasInvalidQuantity(int invalidQuantity)
        {
            var product = ProductTests.GetProduct();

            OrderLine orderLine = new (
                quantity: invalidQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            Assert.IsFalse(orderLine.Valid);
            Assert.AreEqual(1, orderLine.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenOrderLineHasUnitaryPriceWithZero()
        {
            var product = ProductTests.GetProduct();

            decimal invalidUnitaryPrice = 0;

            OrderLine orderLine = new(
                quantity: product.CombinedQuantity,
                unitaryPrice: invalidUnitaryPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            Assert.IsFalse(orderLine.Valid);
            Assert.AreEqual(1, orderLine.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenOrderLineHasNegativeUnitaryPrice()
        {
            var product = ProductTests.GetProduct();

            decimal invalidUnitaryPrice = -0.750m;

            OrderLine orderLine = new(
                quantity: product.CombinedQuantity,
                unitaryPrice: invalidUnitaryPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            Assert.IsFalse(orderLine.Valid);
            Assert.AreEqual(1, orderLine.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenOrderLineHasInvalidUnitaryPrice()
        {
            var product = ProductTests.GetProduct();

            decimal invalidAdditionalCosts = -0.75m;

            OrderLine orderLine = new(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: invalidAdditionalCosts,
                product: product);

            Assert.IsFalse(orderLine.Valid);
            Assert.AreEqual(1, orderLine.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenEditOrderLineWithCorrectInputsData()
        {
            var product = ProductTests.GetProduct();

            OrderLine orderLine = new(
                quantity: product.CombinedQuantity,
                unitaryPrice: product.CombinedPrice,
                additionalCosts: product.AdditionalCosts,
                product: product);

            int updatedQuantity = 30000;
            decimal updatedUnitaryPrice = 0.85m;
            decimal updatedAdditionalCosts = 0;

            Product updatedProduct = new(
                name: "COQUILHA OVAL 2",
                ncmCode: "84804910",
                combinedPrice: updatedUnitaryPrice,
                additionalCosts: updatedAdditionalCosts,
                combinedQuantity: updatedQuantity,
                details: "BANHO DE ZINCO",
                customerId: product.CustomerId);

            orderLine.Edit(
                quantity: updatedQuantity,
                unitaryPrice: updatedUnitaryPrice,
                additionalCosts: updatedAdditionalCosts,
                product: updatedProduct);

            Assert.IsTrue(orderLine.Valid);
            Assert.AreEqual(updatedQuantity, orderLine.Quantity);
            Assert.AreEqual(updatedUnitaryPrice, orderLine.UnitaryPrice);
            Assert.AreEqual(updatedAdditionalCosts, orderLine.AdditionalCosts);
            Assert.AreEqual(updatedProduct, orderLine.Product);
        }
    }
}