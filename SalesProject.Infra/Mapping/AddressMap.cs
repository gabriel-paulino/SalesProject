using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(a => a.Description).
                HasColumnName(AddressConstants.FieldDescription).
                HasMaxLength(100).
                HasColumnType("varchar(100)").
                IsRequired();

            builder.Property(a => a.ZipCode).
                HasColumnName(AddressConstants.FieldZipCode).
                HasMaxLength(18).
                HasColumnType("varchar(18)").
                IsRequired();

            builder.Property(a => a.Type).
                HasColumnName(AddressConstants.FieldType).
                HasConversion<int>().
                IsRequired(false);

            builder.Property(a => a.Street).
                HasColumnName(AddressConstants.FieldStreet).
                HasMaxLength(120).
                HasColumnType("varchar(120)").
                IsRequired();

            builder.Property(a => a.Neighborhood).
                HasColumnName(AddressConstants.FieldNeighborhood).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(a => a.City).
                HasColumnName(AddressConstants.FieldCity).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(a => a.Number).
                HasColumnName(AddressConstants.FieldNumber);

            builder.Property(a => a.State).
                HasColumnName(AddressConstants.FieldState).
                HasMaxLength(2).
                HasColumnType("varchar(2)").
                IsRequired();

            builder.Property(a => a.CustomerId).
                HasColumnName(AddressConstants.FieldCustomerId);

            builder.Ignore(a => a.Notifications);
            builder.Ignore(a => a.Valid);

            builder.HasKey(a => a.Id);
            builder.ToTable(AddressConstants.TableAddress);
        }
    }
}