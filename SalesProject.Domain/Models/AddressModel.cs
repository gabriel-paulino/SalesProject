using SalesProject.Domain.Entities;
using SalesProject.Domain.Entities.Base;
using SalesProject.Domain.Enums;
using System;

namespace SalesProject.Domain.Models
{
    public class AddressModel : Model
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
        public AddressType? Type { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string CodeCity { get; set; }
        public string State { get; set; }
        public Guid CustomerId { get; set; }

        public static implicit operator AddressModel(Address entity) =>
            new AddressModel()
            {
                Id = entity.Id,
                Description = entity.Description,
                ZipCode = entity.ZipCode,
                Type = entity.Type,
                Street = entity.Street,
                Neighborhood = entity.Neighborhood,
                Number = entity.Number,
                City = entity.City,
                State = entity.State,
                CustomerId = entity.CustomerId,
            };

        public static implicit operator Address(AddressModel model) =>
            new Address
            (
                description: model.Description,
                zipCode: model.ZipCode,
                type: model.Type,
                street: model.Street,
                neighborhood: model.Neighborhood,
                number: model.Number,
                city: model.City,
                state: model.State,
                customerId: model.CustomerId,
                id: model.Id
            );
    }
}