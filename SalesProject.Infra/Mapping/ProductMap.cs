using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(p => p.Name).
                HasColumnName(ProductConstants.FieldName).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired();

            builder.Property(p => p.NcmCode).
                HasColumnName(ProductConstants.FieldNcmCode).
                HasMaxLength(15).
                HasColumnType("varchar(15)");

            builder.Property(p => p.CombinedPrice).
                HasColumnName(ProductConstants.FieldCombinedPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(p => p.AdditionalCosts).
                HasColumnName(ProductConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(p => p.CombinedQuantity).
                HasColumnName(ProductConstants.FieldCombinedQuantity).
                IsRequired();

            builder.Property(p => p.Details).
                HasColumnName(ProductConstants.FieldDetails).
                HasMaxLength(500).
                HasColumnType("varchar(500)");

            builder.Property(p => p.CustomerId).
                HasColumnName(ProductConstants.FieldCustomerId).
                IsRequired(false);

            builder.Ignore(p => p.Notifications);
            builder.Ignore(p => p.Valid);

            builder.HasKey(p => p.Id);
            builder.ToTable(ProductConstants.TableProduct);
        }
    }
}