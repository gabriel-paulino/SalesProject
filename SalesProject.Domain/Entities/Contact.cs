using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public Contact() { }

        public Contact(
            string firstName, 
            string lastName, 
            string email, 
            string whatsApp, 
            string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = $"{firstName} {lastName}";
            Email = email;
            WhatsApp = whatsApp;
            Phone = phone;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string WhatsApp { get; private set; }
        public string Phone { get; private set; }
        public Guid ClientId { get; private set; }

        public override void DoBusinesRulesValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}
