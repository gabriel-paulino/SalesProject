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
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.PostingDate).
               HasColumnName(OrderConstants.FieldPostingDate).
               HasColumnType("date").
               IsRequired();

            builder.Property(c => c.DeliveryDate).
                HasColumnName(OrderConstants.FieldDeliveryDate).
                HasColumnType("date").
                IsRequired();

            builder.Property(c => c.Status).
                HasColumnName(OrderConstants.FieldStatus).
                HasConversion<int>();

            builder.Property(c => c.TotalOrder).
                HasColumnName(OrderConstants.FieldTotalOrder).
                HasColumnType("money");

            builder.Property(c => c.Observation).
                HasColumnName(OrderConstants.FieldObservation).
                HasMaxLength(300).
                HasColumnType("varchar(300)");

            builder.Property(c => c.CustomerId).
                HasColumnName(OrderConstants.FieldCustomerId).
                IsRequired(false);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.OrderLines).WithOne().HasForeignKey(fk => fk.OrderId);
            builder.HasOne(c => c.Customer).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.ToTable(OrderConstants.TableOrder);
        }
    }
}