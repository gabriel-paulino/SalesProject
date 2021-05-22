using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class InvoiceMap : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(i => i.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(i => i.ReleaseDate).
                HasColumnName(InvoiceConstants.FieldReleaseDate).
                HasColumnType("date").
                IsRequired();

            builder.Property(i => i.BaseCalcIcms).
                HasColumnName(InvoiceConstants.FieldBaseCalcIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(i => i.TotalIcms).
                HasColumnName(InvoiceConstants.FieldTotalIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(i => i.TotalProducts).
                HasColumnName(InvoiceConstants.FieldTotalProducts).
                HasColumnType("money").
                IsRequired();

            builder.Property(i => i.TotalInvoice).
                HasColumnName(InvoiceConstants.FieldTotalInvoice).
                HasColumnType("money").
                IsRequired();

            builder.Property(i => i.OrderId).
                HasColumnName(InvoiceConstants.FieldOrderId).
                IsRequired(false);

            builder.Ignore(i => i.Notifications);
            builder.Ignore(i => i.Valid);

            builder.HasMany(i => i.InvoiceLines).WithOne();

            builder.HasOne(i => i.Order)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasKey(i => i.Id);
            builder.ToTable(InvoiceConstants.TableInvoice);
        }
    }
}