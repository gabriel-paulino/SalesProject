using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(o => o.PostingDate).
               HasColumnName(OrderConstants.FieldPostingDate).
               HasColumnType("date").
               IsRequired();

            builder.Property(o => o.DeliveryDate).
                HasColumnName(OrderConstants.FieldDeliveryDate).
                HasColumnType("date").
                IsRequired();

            builder.Property(o => o.Status).
                HasColumnName(OrderConstants.FieldStatus).
                HasConversion<int>();

            builder.Property(o => o.TotalOrder).
                HasColumnName(OrderConstants.FieldTotalOrder).
                HasColumnType("money");

            builder.Property(o => o.Observation).
                HasColumnName(OrderConstants.FieldObservation).
                HasMaxLength(300).
                HasColumnType("varchar(300)");

            builder.Property(o => o.CustomerId).
                HasColumnName(OrderConstants.FieldCustomerId).
                IsRequired(false);

            builder.HasIndex(c => new { c.Status, c.PostingDate });

            builder.Ignore(o => o.Notifications);
            builder.Ignore(o => o.Valid);

            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.OrderLines).
                WithOne().
                HasForeignKey(fk => fk.OrderId);

            builder.HasOne(o => o.Customer).
                WithMany().
                OnDelete(DeleteBehavior.SetNull);

            builder.ToTable(OrderConstants.TableOrder);
        }
    }
}