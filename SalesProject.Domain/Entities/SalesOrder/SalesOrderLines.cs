using SalesProject.Domain.Entities.Base;

namespace SalesProject.Domain.Entities
{
    public class SalesOrderLines : BaseEntity
    {
        public SalesOrderLines(
            int idProduct, 
            Product product, 
            double quantity, 
            double price, 
            double? discont, 
            double? additionalCosts, 
            double tax)
        {
            IdProduct = idProduct;
            Product = product;
            Quantity = quantity;
            Price = price;
            Discont = discont;
            AdditionalCosts = additionalCosts;
            Tax = tax;
        }

        public int IdProduct { get; private set; }
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