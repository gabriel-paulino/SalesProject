using SalesProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SalesProject.Context
{
    public class SalesProjectDataContext : DbContext
    {
        public SalesProjectDataContext(DbContextOptions<SalesProjectDataContext> options)
            : base(options) { }

        public DbSet<Address> Adresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
