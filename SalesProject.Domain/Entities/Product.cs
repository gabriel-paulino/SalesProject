using SalesProject.Domain.Entities.Base;

namespace SalesProject.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(
            string name, 
            double scheduledQuantity, 
            string details, 
            int idClient, 
            Client client)
        {
            Name = name;
            ScheduledQuantity = scheduledQuantity;
            Details = details;
            IdClient = idClient;
            Client = client;
        }

        public string Name { get; private set; }
        public double ScheduledQuantity { get; private set; }
        public string Details { get; private set; }
        public int IdClient { get; private set; }
        public Client Client { get; private set; }

        public override void DoBusinesRulesValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}
