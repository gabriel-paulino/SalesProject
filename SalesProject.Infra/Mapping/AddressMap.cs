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
                HasColumnType("uniqueidentifier").
                HasComment(AddressConstants.Id);

            builder.Property(a => a.Description).
                HasColumnName(AddressConstants.FieldDescription).
                HasMaxLength(100).
                HasColumnType("varchar(100)").
                IsRequired().
                HasComment(AddressConstants.Description);

            builder.Property(a => a.ZipCode).
                HasColumnName(AddressConstants.FieldZipCode).
                HasMaxLength(18).
                HasColumnType("varchar(18)").
                IsRequired().
                HasComment(AddressConstants.ZipCode);

            builder.Property(a => a.Type).
                HasColumnName(AddressConstants.FieldType).
                HasConversion<int>().
                IsRequired(false).
                HasComment(AddressConstants.Type);

            builder.Property(a => a.Street).
                HasColumnName(AddressConstants.FieldStreet).
                HasMaxLength(120).
                HasColumnType("varchar(120)").
                IsRequired().
                HasComment(AddressConstants.Street);

            builder.Property(a => a.Neighborhood).
                HasColumnName(AddressConstants.FieldNeighborhood).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired().
                HasComment(AddressConstants.Neighborhood);

            builder.Property(a => a.City).
                HasColumnName(AddressConstants.FieldCity).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired().
                HasComment(AddressConstants.City);

            builder.Property(a => a.CodeCity).
                HasColumnName(AddressConstants.FieldCodeCity).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired().
                HasComment(AddressConstants.CodeCity);

            builder.Property(a => a.Number).
                HasColumnName(AddressConstants.FieldNumber).
                HasComment(AddressConstants.Number);

            builder.Property(a => a.State).
                HasColumnName(AddressConstants.FieldState).
                HasMaxLength(2).
                HasColumnType("varchar(2)").
                IsRequired().
                HasComment(AddressConstants.State);

            builder.Property(a => a.CustomerId).
                HasColumnName(AddressConstants.FieldCustomerId).
                HasComment(AddressConstants.CustomerId);

            builder.Ignore(a => a.Notifications);
            builder.Ignore(a => a.Valid);

            builder.HasKey(a => a.Id);
            builder.ToTable(AddressConstants.TableAddress);
        }
    }
}