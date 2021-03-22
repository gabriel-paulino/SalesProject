using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants;
using SalesProject.Domain.Entities;
using System;

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
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.ScheduledQuantity).
                HasColumnName(ProductConstants.FieldScheduledQuantity).
                IsRequired();

            builder.Property(c => c.Details).
                HasColumnName(ProductConstants.FieldDetails).
                HasMaxLength(300).
                HasColumnType("varchar(300)");

            builder.HasKey(c => c.Id);
            builder.ToTable(ProductConstants.TableProduct);
        }
    }
}