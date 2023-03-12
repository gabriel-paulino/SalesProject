using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public Contact() { }

        public Contact
        (
            string firstName,
            string lastName,
            string email,
            string whatsApp,
            string phone,
            Guid customerId,
            Guid id = default
        )
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = $"{(string.IsNullOrEmpty(lastName) ? firstName : $"{lastName}, {firstName}")}";
            this.Email = email;
            this.WhatsApp = whatsApp;
            this.Phone = phone;
            this.CustomerId = customerId;

            if (id != default)
                base.Id = id;

            DoValidations();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string WhatsApp { get; private set; }
        public string Phone { get; private set; }
        public Guid CustomerId { get; private set; }

        public Contact Edit(
            string firstName,
            string lastName,
            string email,
            string whatsApp,
            string phone)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = $"{(string.IsNullOrEmpty(lastName) ? firstName : $"{lastName}, {firstName}")}";
            this.Email = email;
            this.WhatsApp = whatsApp;
            this.Phone = phone;

            DoValidations();

            return this;
        }

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
        }

        private void ValidateFillingMandatoryFields()
        {
            if (string.IsNullOrEmpty(FirstName))
                AddNotification("O preenchimento do campo 'Nome' é obrigatório.");
            if (string.IsNullOrEmpty(Email))
                AddNotification("O preenchimento do campo 'E-mail' é obrigatório.");
            if (string.IsNullOrEmpty(Phone))
                AddNotification("O preenchimento do campo 'Telefone' é obrigatório.");
        }
    }
}