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
                HasColumnType("uniqueidentifier").
                HasComment(InvoiceConstants.Id);

            builder.Property(i => i.ReleaseDate).
                HasColumnName(InvoiceConstants.FieldReleaseDate).
                HasColumnType("date").
                IsRequired().
                HasComment(InvoiceConstants.ReleaseDate);

            builder.Property(i => i.OriginOperation).
                HasColumnName(InvoiceConstants.FieldOriginOperation).
                HasMaxLength(150).
                HasColumnType("varchar(150)").
                IsRequired().
                HasComment(InvoiceConstants.OriginOperation);

            builder.Property(i => i.BaseCalcIcms).
                HasColumnName(InvoiceConstants.FieldBaseCalcIcms).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceConstants.BaseCalcIcms);

            builder.Property(i => i.TotalIcms).
                HasColumnName(InvoiceConstants.FieldTotalIcms).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceConstants.TotalIcms);

            builder.Property(i => i.TotalProducts).
                HasColumnName(InvoiceConstants.FieldTotalProducts).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceConstants.TotalProducts);

            builder.Property(i => i.TotalInvoice).
                HasColumnName(InvoiceConstants.FieldTotalInvoice).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceConstants.TotalInvoice);

            builder.Property(c => c.IntegratedPlugNotasApi).
                HasColumnName(InvoiceConstants.FieldIntegratedPlugNotasApi).
                HasMaxLength(1).
                HasColumnType("varchar(1)").
                IsRequired().
                HasComment(InvoiceConstants.IntegratedPlugNotasApi);

            builder.Property(i => i.OrderId).
                HasColumnName(InvoiceConstants.FieldOrderId).
                IsRequired(false).
                HasComment(InvoiceConstants.OrderId);

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