using SalesProject.Domain.Entities;
using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Models
{
    public class CustomerModel : Model
    {
        public Guid Id { get; set; }
        public string Cnpj { get; set; }
        public string CompanyName { get; set; }
        public string StateRegistration { get; set; }
        public string Email { get; set; }
        public DateTime? Opening { get; set; }
        public string Phone { get; set; }
        public DateTime? ClientSince { get; set; }
        public string MunicipalRegistration { get; set; }
        public IEnumerable<AddressModel> Adresses { get; set; } = new List<AddressModel>();
        public IEnumerable<ContactModel> Contacts { get; set; } = new List<ContactModel>();
        public IEnumerable<ProductModel> Products { get; set; } = new List<ProductModel>();
        public UserModel? User { get; set; }

        public static implicit operator CustomerModel(Customer entity) =>
            new CustomerModel()
            {
                Id = entity.Id,
                Cnpj = entity.Cnpj,
                CompanyName = entity.CompanyName,
                StateRegistration = entity.StateRegistration,
                Email = entity.Email,
                Opening = entity.Opening,
                Phone = entity.Phone,
                ClientSince = entity.ClientSince,
                MunicipalRegistration = entity.MunicipalRegistration,
                Adresses = entity.Adresses.Select(address => (AddressModel)address),
                Contacts = entity.Contacts.Select(contact => (ContactModel)contact),
                Products = entity.Products.Select(product => (ProductModel)product),
                User = entity.User,
            };

        public static implicit operator Customer(CustomerModel model)
        {
            var customer = new Customer
            (
                cnpj: model.Cnpj,
                companyName: model.CompanyName,
                opening: model.Opening,
                phone: model.Phone,
                municipalRegistration: model.MunicipalRegistration,
                stateRegistration: model.StateRegistration,
                email: model.Email,
                id: model.Id
            );

            if(model?.User is not null)
                customer.SetUser(model.User);

            model.Adresses
                .ToList()
                .ForEach(address => customer.AddAddress(address));

            model.Contacts
                .ToList()
                .ForEach(contact => customer.AddContact(contact));

            model.Products
                .ToList()
                .ForEach(product => customer.AddProduct(product));

            return customer;
        }
    }
}