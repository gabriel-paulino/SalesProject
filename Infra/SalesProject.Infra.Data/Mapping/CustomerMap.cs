using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier").
                HasComment(CustomerConstants.Id);

            builder.Property(c => c.Cnpj).
                HasMaxLength(18).
                HasColumnType("varchar(18)").
                IsRequired().
                HasComment(CustomerConstants.Cnpj);

            builder.Property(c => c.CompanyName).
                HasColumnName(CustomerConstants.FieldCompanyName).
                HasMaxLength(100).
                HasColumnType("varchar(100)").
                IsRequired().
                HasComment(CustomerConstants.CompanyName);

            builder.Property(c => c.Email).
                HasColumnName(CustomerConstants.FieldEmail).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired().
                HasComment(CustomerConstants.Email);

            builder.Property(c => c.StateRegistration).
                HasColumnName(CustomerConstants.FieldStateRegistration).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired().
                HasComment(CustomerConstants.StateRegistration);

            builder.Property(c => c.Opening).
                HasColumnName(CustomerConstants.FieldOpening).
                HasColumnType("date").
                HasComment(CustomerConstants.Opening);

            builder.Property(c => c.Phone).
                HasColumnName(CustomerConstants.FieldTelNumber).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                HasComment(CustomerConstants.Phone);

            builder.Property(c => c.ClientSince).
                HasColumnName(CustomerConstants.FieldClientSince).
                HasColumnType("date").
                HasComment(CustomerConstants.ClientSince);

            builder.Property(c => c.MunicipalRegistration).
                HasColumnName(CustomerConstants.FieldMunicipalRegistration).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                HasComment(CustomerConstants.MunicipalRegistration);

            builder.Ignore(c => c.Adresses);
            builder.Ignore(c => c.Contacts);
            builder.Ignore(c => c.Products);
            builder.Ignore(c => c.User);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasMany(c => c.Adresses).
                WithOne().
                OnDelete(DeleteBehavior.Cascade).
                HasForeignKey(fk => fk.CustomerId);

            builder.HasMany(c => c.Contacts).
                WithOne().
                OnDelete(DeleteBehavior.Cascade).
                HasForeignKey(fk => fk.CustomerId);

            builder.HasMany(c => c.Products).
                WithOne().
                OnDelete(DeleteBehavior.SetNull).
                HasForeignKey(fk => fk.CustomerId);

            builder.HasOne(c => c.User).
                WithOne().
                OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(c => c.Id);
            builder.ToTable(CustomerConstants.TableClient);
        }
    }
}