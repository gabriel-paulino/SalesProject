using SalesProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SalesProject.Mapping;

namespace SalesProject.Context
{
    public class SalesProjectDataContext : DbContext
    {
        public SalesProjectDataContext(DbContextOptions<SalesProjectDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new ContactMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new OrderLinesMap());
            builder.ApplyConfiguration(new InvoiceMap());
            builder.ApplyConfiguration(new InvoiceLinesMap());
        }

        public DbSet<Customer> Clients { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> SalesOrders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}