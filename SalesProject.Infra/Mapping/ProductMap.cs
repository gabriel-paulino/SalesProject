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
                HasColumnType("uniqueidentifier").
                HasComment(ProductConstants.Id);

            builder.Property(p => p.Name).
                HasColumnName(ProductConstants.FieldName).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired().
                HasComment(ProductConstants.Name);

            builder.Property(p => p.NcmCode).
                HasColumnName(ProductConstants.FieldNcmCode).
                HasMaxLength(15).
                HasColumnType("varchar(15)").
                HasComment(ProductConstants.NcmCode);

            builder.Property(p => p.CombinedPrice).
                HasColumnName(ProductConstants.FieldCombinedPrice).
                HasColumnType("money").
                IsRequired().
                HasComment(ProductConstants.CombinedPrice);

            builder.Property(p => p.AdditionalCosts).
                HasColumnName(ProductConstants.FieldAdditionalCosts).
                HasColumnType("money").
                HasComment(ProductConstants.AdditionalCosts);

            builder.Property(p => p.CombinedQuantity).
                HasColumnName(ProductConstants.FieldCombinedQuantity).
                IsRequired().
                HasComment(ProductConstants.CombinedQuantity);

            builder.Property(p => p.Details).
                HasColumnName(ProductConstants.FieldDetails).
                HasMaxLength(500).
                HasColumnType("varchar(500)").
                HasComment(ProductConstants.Details);

            builder.Property(p => p.CustomerId).
                HasColumnName(ProductConstants.FieldCustomerId).
                IsRequired(false).
                HasComment(ProductConstants.CustomerId);

            builder.Ignore(p => p.Notifications);
            builder.Ignore(p => p.Valid);

            builder.HasKey(p => p.Id);
            builder.ToTable(ProductConstants.TableProduct);
        }
    }
}