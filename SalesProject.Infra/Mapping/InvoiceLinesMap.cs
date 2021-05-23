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
            builder.Property(il => il.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(il => il.ItemName).
                HasColumnName(InvoiceLinesConstants.FieldItemName).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired();

            builder.Property(il => il.NcmCode).
                HasColumnName(InvoiceLinesConstants.FieldNcmCode).
                HasMaxLength(15).
                HasColumnType("varchar(15)");

            builder.Property(il => il.Quantity).
               HasColumnName(InvoiceLinesConstants.FieldQuantity).
               IsRequired();

            builder.Property(il => il.UnitaryPrice).
                HasColumnName(InvoiceLinesConstants.FieldUnitaryPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(il => il.TotalPrice).
                HasColumnName(InvoiceLinesConstants.FieldTotalPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(il => il.TotalTax).
                HasColumnName(InvoiceLinesConstants.FieldTotalTax).
                HasColumnType("money").
                IsRequired();

            builder.Property(il => il.AdditionalCosts).
                HasColumnName(InvoiceLinesConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(il => il.ValueBaseCalcIcms).
                HasColumnName(InvoiceLinesConstants.FieldValueBaseCalcIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(il => il.ValueIcms).
                HasColumnName(InvoiceLinesConstants.FieldValueIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(il => il.AliquotIcms).
                HasColumnName(InvoiceLinesConstants.FieldAliquotIcms).
                IsRequired();

            builder.Property(il => il.CstIcms).
                HasColumnName(InvoiceLinesConstants.FieldCstIcms).
                HasMaxLength(3).
                HasColumnType("varchar(3)").
                IsRequired();

            builder.Property(il => il.OriginIcms).
                HasColumnName(InvoiceLinesConstants.FieldOriginIcms).
                HasMaxLength(3).
                HasColumnType("varchar(1)").
                IsRequired();

            builder.Property(il => il.DeterminationMode).
                HasColumnName(InvoiceLinesConstants.FieldDeterminationMode).
                IsRequired();

            builder.Property(il => il.CstPis).
                HasColumnName(InvoiceLinesConstants.FieldCstPis).
                HasMaxLength(3).
                HasColumnType("varchar(3)");

            builder.Property(il => il.ValueBaseCalcPis).
                HasColumnName(InvoiceLinesConstants.FieldValueBaseCalcPis).
                HasColumnType("money");

            builder.Property(il => il.AliquotPis).
               HasColumnName(InvoiceLinesConstants.FieldAliquotPis);

            builder.Property(il => il.ValuePis).
                HasColumnName(InvoiceLinesConstants.FieldValuePis).
                HasColumnType("money");

            builder.Property(il => il.CstCofins).
                HasColumnName(InvoiceLinesConstants.FieldCstCofins).
                HasMaxLength(3).
                HasColumnType("varchar(3)");

            builder.Property(il => il.ValueBaseCalcCofins).
                HasColumnName(InvoiceLinesConstants.FieldValueBaseCalcCofins).
                HasColumnType("money");

            builder.Property(il => il.AliquotCofins).
                HasColumnName(InvoiceLinesConstants.FieldAliquotCofins);

            builder.Property(il => il.ValueCofins).
                HasColumnName(InvoiceLinesConstants.FieldValueCofins).
                HasColumnType("money");

            builder.Property(il => il.InvoiceId).
                HasColumnName(InvoiceLinesConstants.FieldInvoiceId);

            builder.Ignore(il => il.Notifications);
            builder.Ignore(il => il.Valid);

            builder.HasKey(il => il.Id);
            builder.ToTable(InvoiceLinesConstants.TableInvoiceLines);
        }
    }
}