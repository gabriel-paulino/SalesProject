using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
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

            builder.Property(c => c.PercentageUnitaryDiscont).
                HasColumnName(OrderLinesConstants.FieldPercentageUnitaryDiscont);

            builder.Property(c => c.ValueUnitaryDiscont).
                HasColumnName(OrderLinesConstants.FieldValueUnitaryDiscont).
                HasColumnType("money");

            builder.Property(c => c.AdditionalCosts).
                HasColumnName(OrderLinesConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(c => c.Cst).
                HasColumnName(OrderLinesConstants.FieldCst).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(c => c.Cfop).
                HasColumnName(OrderLinesConstants.FieldCfop).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.BaseCalcIcms).
                HasColumnName(OrderLinesConstants.FieldBaseCalcIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IcmsValue).
                HasColumnName(OrderLinesConstants.FieldIcmsValue).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IpiValue).
                HasColumnName(OrderLinesConstants.FieldIpiValue).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IcmsAliquot).
                HasColumnName(OrderLinesConstants.FieldIcmsAliquot).
                IsRequired();

            builder.Property(c => c.IpiAliquot).
                HasColumnName(OrderLinesConstants.FieldIpiAliquot).
                IsRequired();

            builder.Property(c => c.OrderId).
                HasColumnName(OrderLinesConstants.FieldOrderId);

            builder.Property(c => c.ProductId).
                HasColumnName(OrderLinesConstants.FieldProductId).
                IsRequired(false);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);
            builder.Ignore(c => c.TaxLine);

            builder.HasOne(c => c.Product).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.HasKey(c => c.Id);
            builder.ToTable(OrderLinesConstants.TableOrderLines);
        }
    }
}