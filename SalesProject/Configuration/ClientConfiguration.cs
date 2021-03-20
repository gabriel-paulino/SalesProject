using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants;
using SalesProject.Domain.Entities;

namespace SalesProject.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        { 
            builder.ToTable(ClientConstantes.TableClient);

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Cnpj).HasMaxLength(18).IsRequired();
            builder.Property(c => c.CompanyName).HasMaxLength(150).IsRequired();
        }
    }
}
