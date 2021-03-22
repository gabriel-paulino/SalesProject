using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class SalesOrder : BaseEntity
    {
        private IList<SalesOrderLines> _orderLines;

        public SalesOrder(
            int idClient, 
            Client client, 
            DateTime 
            postingDate, 
            double freight, 
            DateTime deliveryDate, 
            double totalTax, 
            double totalDiscount, 
            double totalPriceProducts, 
            string observation)
        {
            IdClient = idClient;
            Client = client;
            PostingDate = postingDate;
            Freight = freight;
            DeliveryDate = deliveryDate;
            TotalTax = totalTax;
            TotalDiscount = totalDiscount;
            TotalPriceProducts = totalPriceProducts;
            Observation = observation;
            _orderLines = new List<SalesOrderLines>();
        }

        public int IdClient { get; private set; }
        public Client Client { get; private set; }
        public DateTime PostingDate { get; private set; }
        public double Freight { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public double TotalTax { get; private set; }
        public double TotalDiscount { get; private set; }
        public double TotalPriceProducts { get; private set; }
        public string Observation { get; private set; }
        public IReadOnlyCollection<SalesOrderLines> OrderLines { get { return _orderLines.ToArray(); } }

        public void AddOrderItem(SalesOrderLines orderLine)
        {
            _orderLines.Add(orderLine);
        }

        public override void DoBusinesRulesValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}