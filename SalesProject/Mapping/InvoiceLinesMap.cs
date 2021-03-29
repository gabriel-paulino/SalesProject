using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class InvoiceLinesMap : IEntityTypeConfiguration<InvoiceLines>
    {
        public void Configure(EntityTypeBuilder<InvoiceLines> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.Quantity).
               HasColumnName(InvoiceLinesConstants.FieldQuantity).
               IsRequired();

            builder.Property(c => c.UnitaryPrice).
                HasColumnName(InvoiceLinesConstants.FieldUnitaryPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.TotalPrice).
                HasColumnName(InvoiceLinesConstants.FieldTotalPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.PercentageUnitaryDiscont).
                HasColumnName(InvoiceLinesConstants.FieldPercentageUnitaryDiscont);

            builder.Property(c => c.ValueUnitaryDiscont).
                HasColumnName(InvoiceLinesConstants.FieldValueUnitaryDiscont).
                HasColumnType("money");

            builder.Property(c => c.AdditionalCosts).
                HasColumnName(InvoiceLinesConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(c => c.Cst).
                HasColumnName(InvoiceLinesConstants.FieldCst).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(c => c.Cfop).
                HasColumnName(InvoiceLinesConstants.FieldCfop).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.BaseCalcIcms).
                HasColumnName(InvoiceLinesConstants.FieldBaseCalcIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IcmsValue).
                HasColumnName(InvoiceLinesConstants.FieldIcmsValue).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IpiValue).
                HasColumnName(InvoiceLinesConstants.FieldIpiValue).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IcmsAliquot).
                HasColumnName(InvoiceLinesConstants.FieldIcmsAliquot).
                IsRequired();

            builder.Property(c => c.IpiAliquot).
                HasColumnName(InvoiceLinesConstants.FieldIpiAliquot).
                IsRequired();

            builder.Property(c => c.InvoiceId).
                HasColumnName(InvoiceLinesConstants.FieldInvoiceId);

            builder.Property(c => c.ProductId).
                HasColumnName(InvoiceLinesConstants.FieldProductId).
                IsRequired(false);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);
            builder.Ignore(c => c.TaxLine);

            builder.HasOne(c => c.Product).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.HasKey(c => c.Id);
            builder.ToTable(InvoiceLinesConstants.TableInvoiceLines);
        }
    }
}