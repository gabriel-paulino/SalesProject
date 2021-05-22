using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class InvoiceLinesMap : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
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

            builder.Property(c => c.TotalTax).
                HasColumnName(InvoiceLinesConstants.FieldTotalTax).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.AdditionalCosts).
                HasColumnName(InvoiceLinesConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(c => c.ValueBaseCalcIcms).
                HasColumnName(InvoiceLinesConstants.FieldValueBaseCalcIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.ValueIcms).
                HasColumnName(InvoiceLinesConstants.FieldValueIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.AliquotIcms).
                HasColumnName(InvoiceLinesConstants.FieldAliquotIcms).
                IsRequired();

            builder.Property(c => c.CstIcms).
                HasColumnName(InvoiceLinesConstants.FieldCstIcms).
                HasMaxLength(3).
                HasColumnType("varchar(3)").
                IsRequired();

            builder.Property(c => c.OriginIcms).
                HasColumnName(InvoiceLinesConstants.FieldOriginIcms).
                HasMaxLength(3).
                HasColumnType("varchar(1)").
                IsRequired();

            builder.Property(c => c.DeterminationMode).
                HasColumnName(InvoiceLinesConstants.FieldDeterminationMode).
                IsRequired();

            builder.Property(c => c.CstPis).
                HasColumnName(InvoiceLinesConstants.FieldCstPis).
                HasMaxLength(3).
                HasColumnType("varchar(3)");

            builder.Property(c => c.ValueBaseCalcPis).
                HasColumnName(InvoiceLinesConstants.FieldValueBaseCalcPis).
                HasColumnType("money");

            builder.Property(c => c.QuantityBaseCalcPis).
                HasColumnName(InvoiceLinesConstants.FieldQuantityBaseCalcPis);

            builder.Property(c => c.AliquotPis).
               HasColumnName(InvoiceLinesConstants.FieldAliquotPis);

            builder.Property(c => c.ValuePis).
                HasColumnName(InvoiceLinesConstants.FieldValuePis).
                HasColumnType("money");

            builder.Property(c => c.CstCofins).
                HasColumnName(InvoiceLinesConstants.FieldCstCofins).
                HasMaxLength(3).
                HasColumnType("varchar(3)");

            builder.Property(c => c.ValueBaseCalcCofins).
                HasColumnName(InvoiceLinesConstants.FieldValueBaseCalcCofins).
                HasColumnType("money");

            builder.Property(c => c.AliquotCofins).
                HasColumnName(InvoiceLinesConstants.FieldAliquotCofins);

            builder.Property(c => c.ValueCofins).
                HasColumnName(InvoiceLinesConstants.FieldValueCofins).
                HasColumnType("money");

            builder.Property(c => c.InvoiceId).
                HasColumnName(InvoiceLinesConstants.FieldInvoiceId);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasKey(c => c.Id);
            builder.ToTable(InvoiceLinesConstants.TableInvoiceLines);
        }
    }
}