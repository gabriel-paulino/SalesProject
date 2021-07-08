using SalesProject.Domain.Entities.Base;
using SalesProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            this.Status = OrderStatus.Open;
            _orderLines = new List<OrderLine>();
        }

        public Order(
            DateTime postingDate,
            DateTime deliveryDate,
            string observation,
            Customer customer)
        {
            this.Customer = customer;
            this.PostingDate = postingDate;
            this.DeliveryDate = deliveryDate;
            this.Status = OrderStatus.Open;
            this.Observation = observation;
            _orderLines = new List<OrderLine>();

            DoValidations();
        }

        private IList<OrderLine> _orderLines;

        public DateTime PostingDate { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalOrder { get; private set; }
        public string Observation { get; private set; }
        public IReadOnlyCollection<OrderLine> OrderLines { get => _orderLines.ToArray(); }
        public Customer Customer { get; private set; }
        public Guid? CustomerId { get; private set; }


        public Order Edit(
            DateTime postingDate,
            DateTime deliveryDate,
            string observation)
        {
            this.PostingDate = postingDate;
            this.DeliveryDate = deliveryDate;
            this.Observation = observation;

            DoValidations();

            return this;
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            _orderLines.Add(orderLine);
            UpdateTotalOrder();
        }

        public void RemoveOrderLine(OrderLine orderLine)
        {
            _orderLines.Remove(orderLine);
            UpdateTotalOrder();
        }

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
        }

        private void ValidateFillingMandatoryFields()
        {
            if (Customer is null)
                AddNotification("O preenchimento do campo 'Cliente' é obrigatório.");
        }

        public void UpdateTotalOrder() =>
            this.TotalOrder = OrderLines.Sum(l => l.TotalPrice);

        public void Cancel()
        {
            if (Status == OrderStatus.Open)
            {
                Status = OrderStatus.Canceled;
                return;
            }
            AddNotification("Não é possível cancelar esse pedido de venda.");              
        }

        public void Approve()
        {
            if (Status == OrderStatus.Open)
            {
                Status = OrderStatus.Approved;
                return;
            }
            AddNotification("Não é possível aprovar esse pedido de venda.");
        }

        public void Bill()
        {
            if (Status == OrderStatus.Approved)
            {
                Status = OrderStatus.Billed;
                return;
            }
            AddNotification("Não é possível faturar esse pedido de venda.");
        }

        public bool CanBillThisOrder() => Status == OrderStatus.Approved;
    }
}