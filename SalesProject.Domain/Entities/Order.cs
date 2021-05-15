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
            if (PostingDate == null)
                AddNotification("O preenchimento do campo 'Data de lançamento' é obrigatório.");
            if (DeliveryDate == null)
                AddNotification("O preenchimento do campo 'Data de entrega' é obrigatório.");
            if (Customer == null)
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
    }
}