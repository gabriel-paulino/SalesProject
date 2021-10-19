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
                HasColumnType("uniqueidentifier").
                HasComment(OrderConstants.Id);

            builder.Property(o => o.PostingDate).
               HasColumnName(OrderConstants.FieldPostingDate).
               HasColumnType("date").
               IsRequired().
               HasComment(OrderConstants.PostingDate);

            builder.Property(o => o.DeliveryDate).
                HasColumnName(OrderConstants.FieldDeliveryDate).
                HasColumnType("date").
                IsRequired().
                HasComment(OrderConstants.DeliveryDate);

            builder.Property(o => o.Status).
                HasColumnName(OrderConstants.FieldStatus).
                HasConversion<int>().
                HasComment(OrderConstants.Status);

            builder.Property(o => o.TotalOrder).
                HasColumnName(OrderConstants.FieldTotalOrder).
                HasColumnType("money").
                HasComment(OrderConstants.TotalOrder);

            builder.Property(o => o.Observation).
                HasColumnName(OrderConstants.FieldObservation).
                HasMaxLength(300).
                HasColumnType("varchar(300)").
                HasComment(OrderConstants.Observation);

            builder.Property(o => o.CustomerId).
                HasColumnName(OrderConstants.FieldCustomerId).
                IsRequired(false).
                HasComment(OrderConstants.CustomerId);

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