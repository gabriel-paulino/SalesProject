using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order() { }

        public Order(
            DateTime postingDate,
            DateTime deliveryDate,
            string observation,
            Customer customer)
        {
            Customer = customer;
            PostingDate = postingDate;
            DeliveryDate = deliveryDate;
            Observation = observation;
            _orderLines = new List<OrderLines>();

            DoValidations();
        }

        private IList<OrderLines> _orderLines;

        public DateTime PostingDate { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public decimal Freight { get; private set; }
        public decimal TotalTax { get; private set; }
        public decimal TotalDiscount { get; private set; }
        public decimal TotalPriceProducts { get; private set; }
        public decimal TotalOrder { get; private set; }
        public string Observation { get; private set; }
        public IReadOnlyCollection<OrderLines> OrderLines { get => _orderLines.ToArray(); }
        public Customer Customer { get; private set; }
        public Guid CustomerId { get; private set; }

        public void AddOrderLine(OrderLines orderLine)
        {
            _orderLines.Add(orderLine);
            UpdateOrderValues();
        }

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
        }

        private void ValidateFillingMandatoryFields()
        {
            if (PostingDate == null || PostingDate == DateTime.MinValue)
                AddNotification("O preenchimento do campo 'Data de lançamento' é obrigatório.");
            if (DeliveryDate == null || DeliveryDate == DateTime.MinValue)
                AddNotification("O preenchimento do campo 'Data de entrega' é obrigatório.");
            if (Customer == null)
                AddNotification("O preenchimento do campo 'Cliente' é obrigatório.");
        }

        private void UpdateOrderValues()
        {
            //Atualizar Frete, Imposto, Produtos, Desconto e Valor total basedo nas linhas
            //Em debug verificar se será necessario resetar os totais antes do foreach
        }
    }
}