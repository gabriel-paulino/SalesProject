using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class InvoiceLineMap : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.Property(il => il.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier")
                .HasComment(InvoiceLineConstants.Id);

            builder.Property(il => il.ItemName).
                HasColumnName(InvoiceLineConstants.FieldItemName).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired().
                HasComment(InvoiceLineConstants.ItemName);

            builder.Property(il => il.NcmCode).
                HasColumnName(InvoiceLineConstants.FieldNcmCode).
                HasMaxLength(15).
                HasColumnType("varchar(15)").
                HasComment(InvoiceLineConstants.NcmCode);

            builder.Property(il => il.Quantity).
               HasColumnName(InvoiceLineConstants.FieldQuantity).
               IsRequired().
               HasComment(InvoiceLineConstants.Quantity);

            builder.Property(il => il.UnitaryPrice).
                HasColumnName(InvoiceLineConstants.FieldUnitaryPrice).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceLineConstants.UnitaryPrice);

            builder.Property(il => il.TotalPrice).
                HasColumnName(InvoiceLineConstants.FieldTotalPrice).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceLineConstants.TotalPrice);

            builder.Property(il => il.TotalTax).
                HasColumnName(InvoiceLineConstants.FieldTotalTax).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceLineConstants.TotalTax);

            builder.Property(il => il.AdditionalCosts).
                HasColumnName(InvoiceLineConstants.FieldAdditionalCosts).
                HasColumnType("money").
                HasComment(InvoiceLineConstants.AdditionalCosts);

            builder.Property(il => il.ValueBaseCalcIcms).
                HasColumnName(InvoiceLineConstants.FieldValueBaseCalcIcms).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceLineConstants.ValueBaseCalcIcms);

            builder.Property(il => il.ValueIcms).
                HasColumnName(InvoiceLineConstants.FieldValueIcms).
                HasColumnType("money").
                IsRequired().
                HasComment(InvoiceLineConstants.ValueIcms);

            builder.Property(il => il.AliquotIcms).
                HasColumnName(InvoiceLineConstants.FieldAliquotIcms).
                IsRequired().
                HasComment(InvoiceLineConstants.AliquotIcms);

            builder.Property(il => il.CstIcms).
                HasColumnName(InvoiceLineConstants.FieldCstIcms).
                HasMaxLength(3).
                HasColumnType("varchar(3)").
                IsRequired().
                HasComment(InvoiceLineConstants.CstIcms);

            builder.Property(il => il.OriginIcms).
                HasColumnName(InvoiceLineConstants.FieldOriginIcms).
                HasMaxLength(3).
                HasColumnType("varchar(1)").
                IsRequired().
                HasComment(InvoiceLineConstants.OriginIcms);

            builder.Property(il => il.DeterminationMode).
                HasColumnName(InvoiceLineConstants.FieldDeterminationMode).
                IsRequired().
                HasComment(InvoiceLineConstants.DeterminationMode);

            builder.Property(il => il.CstPis).
                HasColumnName(InvoiceLineConstants.FieldCstPis).
                HasMaxLength(3).
                HasColumnType("varchar(3)").
                HasComment(InvoiceLineConstants.CstPis);

            builder.Property(il => il.ValueBaseCalcPis).
                HasColumnName(InvoiceLineConstants.FieldValueBaseCalcPis).
                HasColumnType("money").
                HasComment(InvoiceLineConstants.ValueBaseCalcPis);

            builder.Property(il => il.AliquotPis).
               HasColumnName(InvoiceLineConstants.FieldAliquotPis).
               HasComment(InvoiceLineConstants.AliquotPis);

            builder.Property(il => il.ValuePis).
                HasColumnName(InvoiceLineConstants.FieldValuePis).
                HasColumnType("money").
                HasComment(InvoiceLineConstants.ValuePis);

            builder.Property(il => il.CstCofins).
                HasColumnName(InvoiceLineConstants.FieldCstCofins).
                HasMaxLength(3).
                HasColumnType("varchar(3)").
                HasComment(InvoiceLineConstants.CstCofins);

            builder.Property(il => il.ValueBaseCalcCofins).
                HasColumnName(InvoiceLineConstants.FieldValueBaseCalcCofins).
                HasColumnType("money").
                HasComment(InvoiceLineConstants.ValueBaseCalcCofins);

            builder.Property(il => il.AliquotCofins).
                HasColumnName(InvoiceLineConstants.FieldAliquotCofins).
                HasComment(InvoiceLineConstants.AliquotCofins);

            builder.Property(il => il.ValueCofins).
                HasColumnName(InvoiceLineConstants.FieldValueCofins).
                HasColumnType("money").
                HasComment(InvoiceLineConstants.ValueCofins);

            builder.Property(il => il.InvoiceId).
                HasColumnName(InvoiceLineConstants.FieldInvoiceId).
                HasComment(InvoiceLineConstants.InvoiceId);

            builder.Ignore(il => il.Notifications);
            builder.Ignore(il => il.Valid);

            builder.HasKey(il => il.Id);
            builder.ToTable(InvoiceLineConstants.TableInvoiceLines);
        }
    }
}