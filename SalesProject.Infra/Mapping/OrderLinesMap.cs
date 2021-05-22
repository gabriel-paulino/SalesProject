using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class OrderLinesMap : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.Property(ol => ol.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(ol => ol.Quantity).
               HasColumnName(OrderLinesConstants.FieldQuantity).
               IsRequired();

            builder.Property(ol => ol.UnitaryPrice).
                HasColumnName(OrderLinesConstants.FieldUnitaryPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(ol => ol.TotalPrice).
                HasColumnName(OrderLinesConstants.FieldTotalPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(ol => ol.AdditionalCosts).
                HasColumnName(OrderLinesConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(ol => ol.OrderId).
                HasColumnName(OrderLinesConstants.FieldOrderId);

            builder.Property(ol => ol.ProductId).
                HasColumnName(OrderLinesConstants.FieldProductId).
                IsRequired(false);

            builder.Ignore(ol => ol.Notifications);
            builder.Ignore(ol => ol.Valid);

            builder.HasOne(ol => ol.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasKey(ol => ol.Id);
            builder.ToTable(OrderLinesConstants.TableOrderLines);
        }
    }
}