using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class SalesOrderLines : BaseEntity
    {
        public SalesOrderLines() { }

        public SalesOrderLines(
            Product product, 
            double quantity, 
            double price, 
            double? discont, 
            double? additionalCosts, 
            double tax)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
            Discont = discont;
            AdditionalCosts = additionalCosts;
            Tax = tax;
        }

        public Product Product { get; private set; }
        public double Quantity { get; private set; }
        public double Price { get; private set; }
        public double? Discont { get; private set; }
        public double? AdditionalCosts { get; private set; }
        public double Tax { get; private set; }

        public override void DoBusinesRulesValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}