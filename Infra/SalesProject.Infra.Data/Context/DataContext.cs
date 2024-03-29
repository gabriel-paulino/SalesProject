﻿using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Infra.Mapping;

namespace SalesProject.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new CustomerMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new ContactMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new OrderLineMap());
            builder.ApplyConfiguration(new InvoiceMap());
            builder.ApplyConfiguration(new InvoiceLineMap());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addreses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}