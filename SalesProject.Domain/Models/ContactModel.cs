using SalesProject.Domain.Entities;
using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Models
{
    public class ContactModel : Model
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string WhatsApp { get; set; }
        public string Phone { get; set; }
        public Guid CustomerId { get; set; }


        public static implicit operator ContactModel(Contact entity) =>
           new ContactModel()
           {
               Id = entity.Id,
               FirstName = entity.FirstName,
               LastName = entity.LastName,
               FullName = entity.FullName,
               Email = entity.Email,
               WhatsApp = entity.WhatsApp,
               Phone = entity.Phone,
               CustomerId = entity.CustomerId
           };

        public static implicit operator Contact(ContactModel model) =>
          new Contact
          (
              firstName: model.FirstName,
              lastName: model.LastName,
              email: model.Email,
              whatsApp: model.WhatsApp,
              phone: model.Phone,
              customerId: model.CustomerId,
              id: model.Id
          );
    }
}
