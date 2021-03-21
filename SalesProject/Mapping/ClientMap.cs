using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        { 
            builder.ToTable(ClientConstantes.TableClient);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Cnpj).HasMaxLength(18).IsRequired();

            builder.Property(c => c.CompanyName).
                HasColumnName(ClientConstantes.FieldCompanyName).
                HasMaxLength(100).
                HasColumnType("varchar(100)").
                IsRequired();

            builder.Property(c => c.StateRegistration).
                HasColumnName(ClientConstantes.FieldStateRegistration).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.Opening).
                HasColumnName(ClientConstantes.FieldOpening);

            builder.Property(c => c.Phone).
                HasColumnName(ClientConstantes.FieldTelNumber).
                HasColumnType("varchar(20)");

            builder.Property(c => c.ClientSince).
                HasColumnName(ClientConstantes.FieldClientSince);

            builder.Property(c => c.MunicipalRegistration).
                HasColumnName(ClientConstantes.FieldMunicipalRegistration).
                HasColumnType("varchar(30)");

        }
    }
}
