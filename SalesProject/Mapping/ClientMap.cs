using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        { 
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.Cnpj).
                HasMaxLength(18).
                HasColumnType("varchar(18)").
                IsRequired();

            builder.Property(c => c.CompanyName).
                HasColumnName(ClientConstants.FieldCompanyName).
                HasMaxLength(100).
                HasColumnType("varchar(100)").
                IsRequired();

            builder.Property(c => c.StateRegistration).
                HasColumnName(ClientConstants.FieldStateRegistration).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.Opening).
                HasColumnName(ClientConstants.FieldOpening).
                HasColumnType("date");

            builder.Property(c => c.Phone).
                HasColumnName(ClientConstants.FieldTelNumber).
                HasMaxLength(20).
                HasColumnType("varchar(20)");

            builder.Property(c => c.ClientSince).
                HasColumnName(ClientConstants.FieldClientSince).
                HasColumnType("date");

            builder.Property(c => c.MunicipalRegistration).
                HasColumnName(ClientConstants.FieldMunicipalRegistration).
                HasMaxLength(30).
                HasColumnType("varchar(30)");

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasMany(c => c.Adresses).WithOne();
            builder.HasMany(c => c.Contacts).WithOne();

            builder.HasKey(c => c.Id);
            builder.ToTable(ClientConstants.TableClient);
        }
    }
}