using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.Name).
                HasColumnName(ProductConstants.FieldName).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired();

            builder.Property(c => c.NcmCode).
                HasColumnName(ProductConstants.FieldNcmCode).
                HasMaxLength(15).
                HasColumnType("varchar(15)");

            builder.Property(c => c.CombinedPrice).
                HasColumnName(ProductConstants.FieldCombinedPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.AdditionalCosts).
                HasColumnName(ProductConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(c => c.CombinedQuantity).
                HasColumnName(ProductConstants.FieldCombinedQuantity).
                IsRequired();

            builder.Property(c => c.Details).
                HasColumnName(ProductConstants.FieldDetails).
                HasMaxLength(500).
                HasColumnType("varchar(500)");

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasOne(c => c.Client).WithMany(); //Um Produto tem um cliente, esse cliente tem muitos produtos (é isso ?)

            builder.HasKey(c => c.Id);
            builder.ToTable(ProductConstants.TableProduct);
        }
    }
}