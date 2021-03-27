using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class SalesOrder : BaseEntity
    {
        public SalesOrder() { }

        public SalesOrder(
            DateTime postingDate,
            DateTime deliveryDate,
            string observation,
            Client client)
        {
            Client = client;
            PostingDate = postingDate;
            DeliveryDate = deliveryDate;
            Observation = observation;
            _orderLines = new List<SalesOrderLines>();

            DoValidations();
        }

        private IList<SalesOrderLines> _orderLines;

        public DateTime PostingDate { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public decimal Freight { get; private set; }
        public decimal TotalTax { get; private set; }
        public decimal TotalDiscount { get; private set; }
        public decimal TotalPriceProducts { get; private set; }
        public decimal TotalOrder { get; private set; }
        public string Observation { get; private set; }
        public IReadOnlyCollection<SalesOrderLines> OrderLines { get => _orderLines.ToArray(); }
        public Client Client { get; private set; }
        public Guid ClientId { get; private set; }

        public void AddOrderLine(SalesOrderLines orderLine)
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
            if (Client == null)
                AddNotification("O preenchimento do campo 'Cliente' é obrigatório.");
        }

        private void UpdateOrderValues()
        {
            //Atualizar Frete, Imposto, Produtos, Desconto e Valor total basedo nas linhas
            //Em debug verificar se será necessario resetar os totais antes do foreach
        }
    }
}