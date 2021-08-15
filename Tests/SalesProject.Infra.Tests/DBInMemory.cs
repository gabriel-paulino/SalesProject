using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Infra.Context;
using System;
using System.Linq;

namespace SalesProject.Infra.Tests
{
    public class DBInMemory
    {
        private DataContext _fakeContext;

        public DBInMemory()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connection)
                .EnableSensitiveDataLogging()
                .Options;

            _fakeContext = new DataContext(options);

            InsertFakeData();
        }

        public DataContext GetContext() => _fakeContext;

        private void InsertFakeData()
        {
            if (_fakeContext.Database.EnsureCreated())
            {
                InsertFakeCustomer();

                _fakeContext.SaveChanges();
            }
        }

        private void InsertFakeCustomer()
        {
            var customer = new Customer(
                cnpj: "95098349000114",
                companyName: "Tânia e Aurora Corretores Associados ME",
                opening: DateTime.Parse("03/04/2016"),
                phone: "(11) 3844-7566",
                municipalRegistration: string.Empty,
                stateRegistration: "553.178.021.386",
                email: "tania.corretora@gmail.com"
                );

            var address1 = new Address(
                description: "Escritório",
                zipCode: "02123044",
                type: AddressType.Billing,
                street: "Travessa Gaspar Raposo",
                neighborhood: "Jardim Japão",
                number: 300,
                city: "São Paulo",
                state: "SP",
                customerId: customer.Id);

            address1.SetCodeCity("3550308");

            var address2 = new Address(
                description: "Balcão Principal",
                zipCode: "01033-000",
                type: AddressType.Delivery,
                street: "Avenida Cásper Líbero",
                neighborhood: "Centro",
                number: 324,
                city: "São Paulo",
                state: "SP",
                customerId: customer.Id);

            address2.SetCodeCity("3550308");

            var address3 = new Address(
                description: "Depósito Devolução",
                zipCode: "18046-430",
                type: AddressType.Other,
                street: "Rua Elysio de Oliveira",
                neighborhood: "Jardim São Carlos",
                number: 329,
                city: "Sorocaba",
                state: "SP",
                customerId: customer.Id);

            address3.SetCodeCity("3552205");

            customer.AddAddress(address1);
            customer.AddAddress(address2);
            customer.AddAddress(address3);

            _fakeContext.Customers.Add(customer);
        }
    }
}