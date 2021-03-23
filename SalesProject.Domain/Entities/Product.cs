using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product() { }

        public Product(
            string name, 
            double scheduledQuantity, 
            string details, 
            Client client)
        {
            Name = name;
            ScheduledQuantity = scheduledQuantity;
            Details = details;
            Client = client;
        }

        public string Name { get; private set; }
        public double ScheduledQuantity { get; private set; }
        public string Details { get; private set; }
        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }

        public override void DoBusinesRulesValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}
