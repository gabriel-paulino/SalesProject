using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class OrderLinesMap : IEntityTypeConfiguration<OrderLines>
    {
        public void Configure(EntityTypeBuilder<OrderLines> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.Quantity).
               HasColumnName(OrderLinesConstants.FieldQuantity).
               IsRequired();

            builder.Property(c => c.UnitaryPrice).
                HasColumnName(OrderLinesConstants.FieldUnitaryPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.TotalPrice).
                HasColumnName(OrderLinesConstants.FieldTotalPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.AdditionalCosts).
                HasColumnName(OrderLinesConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(c => c.OrderId).
                HasColumnName(OrderLinesConstants.FieldOrderId);

            builder.Property(c => c.ProductId).
                HasColumnName(OrderLinesConstants.FieldProductId).
                IsRequired(false);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasOne(c => c.Product).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.HasKey(c => c.Id);
            builder.ToTable(OrderLinesConstants.TableOrderLines);
        }
    }
}