using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.ZipCode).
                HasColumnName(AddressConstants.FieldZipCode).
                HasMaxLength(18).
                HasColumnType("varchar(18)").
                IsRequired();

            builder.Property(c => c.Street).
                HasColumnName(AddressConstants.FieldStreet).
                HasMaxLength(120).
                HasColumnType("varchar(120)").
                IsRequired();

            builder.Property(c => c.Neighborhood).
                HasColumnName(AddressConstants.FieldNeighborhood).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.City).
                HasColumnName(AddressConstants.FieldCity).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(c => c.Number).
                HasColumnName(AddressConstants.FieldNumber);

            builder.Property(c => c.State).
                HasColumnName(AddressConstants.FieldState).
                HasMaxLength(2).
                HasColumnType("varchar(2)").
                IsRequired();

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasKey(c => c.Id);
            builder.ToTable(AddressConstants.TableAddress);
        }
    }
}