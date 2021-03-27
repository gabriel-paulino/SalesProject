using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class SalesOrderMap : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.PostingDate).
               HasColumnName(SalesOrderConstants.FieldPostingDate).
               HasColumnType("date").
               IsRequired();

            builder.Property(c => c.DeliveryDate).
                HasColumnName(SalesOrderConstants.FieldDeliveryDate).
                HasColumnType("date").
                IsRequired();

            builder.Property(c => c.Freight).
                HasColumnName(SalesOrderConstants.FieldFreight).
                HasColumnType("money");

            builder.Property(c => c.TotalTax).
                HasColumnName(SalesOrderConstants.FieldTotalTax).
                HasColumnType("money");

            builder.Property(c => c.TotalDiscount).
                HasColumnName(SalesOrderConstants.FieldTotalDiscount).
                HasColumnType("money");

            builder.Property(c => c.TotalPriceProducts).
                HasColumnName(SalesOrderConstants.FieldTotalPriceProducts).
                HasColumnType("money");

            builder.Property(c => c.TotalOrder).
                HasColumnName(SalesOrderConstants.FieldTotalOrder).
                HasColumnType("money");

            builder.Property(c => c.Observation).
                HasColumnName(SalesOrderConstants.FieldObservation).
                HasMaxLength(300).
                HasColumnType("varchar(300)");

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.OrderLines).WithOne();

            builder.ToTable(SalesOrderConstants.TableSalesOrder);
        }
    }
}