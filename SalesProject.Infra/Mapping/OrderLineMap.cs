using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class OrderLineMap : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.Property(ol => ol.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier").
                HasComment(OrderLineConstants.Id);

            builder.Property(ol => ol.Quantity).
               HasColumnName(OrderLineConstants.FieldQuantity).
               IsRequired().
               HasComment(OrderLineConstants.Quantity);

            builder.Property(ol => ol.UnitaryPrice).
                HasColumnName(OrderLineConstants.FieldUnitaryPrice).
                HasColumnType("money").
                IsRequired().
                HasComment(OrderLineConstants.UnitaryPrice);

            builder.Property(ol => ol.TotalPrice).
                HasColumnName(OrderLineConstants.FieldTotalPrice).
                HasColumnType("money").
                IsRequired().
                HasComment(OrderLineConstants.TotalPrice);

            builder.Property(ol => ol.AdditionalCosts).
                HasColumnName(OrderLineConstants.FieldAdditionalCosts).
                HasColumnType("money").
                HasComment(OrderLineConstants.AdditionalCosts);

            builder.Property(ol => ol.OrderId).
                HasColumnName(OrderLineConstants.FieldOrderId).
                HasComment(OrderLineConstants.OrderId);

            builder.Property(ol => ol.ProductId).
                HasColumnName(OrderLineConstants.FieldProductId).
                IsRequired(false).
                HasComment(OrderLineConstants.ProductId);

            builder.Ignore(ol => ol.Notifications);
            builder.Ignore(ol => ol.Valid);

            builder.HasOne(ol => ol.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasKey(ol => ol.Id);
            builder.ToTable(OrderLineConstants.TableOrderLines);
        }
    }
}