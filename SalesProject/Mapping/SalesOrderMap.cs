using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants;
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
               IsRequired();

            builder.Property(c => c.Freight).
                HasColumnName(SalesOrderConstants.FieldFreight);

            builder.Property(c => c.DeliveryDate).
                HasColumnName(SalesOrderConstants.FieldDeliveryDate).
                IsRequired();

            builder.Property(c => c.TotalTax).
                HasColumnName(SalesOrderConstants.FieldTotalTax).
                IsRequired();

            builder.Property(c => c.TotalDiscount).
                HasColumnName(SalesOrderConstants.FieldTotalDiscount);

            builder.Property(c => c.TotalPriceProducts).
                HasColumnName(SalesOrderConstants.FieldTotalPriceProducts).
                IsRequired();

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