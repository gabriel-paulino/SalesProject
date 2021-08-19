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
            var order = GetOrder();

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

        public static Order GetOrder(
            DateTime postingDate = default(DateTime),
            DateTime deliveryDate = default(DateTime),
            string observation = "Observation",
            Customer customer = default(Customer))
            => new Order(
                postingDate: postingDate == DateTime.MinValue ? DateTime.Today : postingDate,
                deliveryDate: deliveryDate == DateTime.MinValue ? DateTime.Today.AddMonths(1) : deliveryDate,
                observation: observation,
                customer: customer ?? CustomerTests.GetCustomer());
    }
}