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
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.Number).
               HasColumnName(InvoiceConstants.FieldNumber).
               IsRequired();

            builder.Property(c => c.Series).
                HasColumnName(InvoiceConstants.FieldSeries).
                IsRequired();

            builder.Property(c => c.OriginOperation).
                HasColumnName(InvoiceConstants.FieldOriginOperation).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(c => c.ReleaseDate).
                HasColumnName(InvoiceConstants.FieldReleaseDate).
                HasColumnType("date").
                IsRequired();

            builder.Property(c => c.LeaveDate).
                HasColumnName(InvoiceConstants.FieldLeaveDate).
                HasColumnType("date").
                IsRequired();

            builder.Property(c => c.LeaveHour).
                HasColumnName(InvoiceConstants.FieldLeaveHour).
                HasMaxLength(30).
                HasColumnType("varchar(30)");

            builder.Property(c => c.CarrierName).
                HasColumnName(InvoiceConstants.FieldCarrierName).
                HasMaxLength(100).
                HasColumnType("varchar(100)").
                IsRequired();

            builder.Property(c => c.PaidBy).
                HasColumnName(InvoiceConstants.FieldPaidBy).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(c => c.AnttCode).
                HasColumnName(InvoiceConstants.FieldAnttCode).
                HasMaxLength(10).
                HasColumnType("varchar(10)");

            builder.Property(c => c.LicensePlate).
                HasColumnName(InvoiceConstants.FieldLicensePlate).
                HasMaxLength(10).
                HasColumnType("varchar(10)");

            builder.Property(c => c.CarrierCnpj).
                HasColumnName(InvoiceConstants.FieldCarrierCnpj).
                HasMaxLength(18).
                HasColumnType("varchar(18)").
                IsRequired();

            builder.Property(c => c.StateRegistration).
                HasColumnName(InvoiceConstants.FieldStateRegistration).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.Quantity).
                HasColumnName(InvoiceConstants.FieldQuantity).
                IsRequired();

            builder.Property(c => c.Type).
                HasColumnName(InvoiceConstants.FieldType).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.Branch).
                HasColumnName(InvoiceConstants.FieldBranch).
                HasMaxLength(20).
                HasColumnType("varchar(20)");

            builder.Property(c => c.Numeration).
                HasColumnName(InvoiceConstants.FieldNumeration).
                HasMaxLength(20).
                HasColumnType("varchar(20)");

            builder.Property(c => c.GrossWeight).
                HasColumnName(InvoiceConstants.FieldGrossWeight);

            builder.Property(c => c.NetWeight).
                HasColumnName(InvoiceConstants.FieldNetWeight);

            builder.Property(c => c.CarrierAddress).
                HasColumnName(InvoiceConstants.FieldCarrierAddress).
                HasMaxLength(160).
                HasColumnType("varchar(160)").
                IsRequired();

            builder.Property(c => c.CarrierCity).
                HasColumnName(InvoiceConstants.FieldCarrierCity).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(c => c.CarrierState).
                HasColumnName(InvoiceConstants.FieldCarrierState).
                HasMaxLength(2).
                HasColumnType("varchar(2)").
                IsRequired();

            builder.Property(c => c.BaseCalcIcms).
                HasColumnName(InvoiceConstants.FieldBaseCalcIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.TotalIcms).
                HasColumnName(InvoiceConstants.FieldTotalIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.BaseCalcOtherIcms).
                HasColumnName(InvoiceConstants.FieldBaseCalcOtherIcms).
                HasColumnType("money");

            builder.Property(c => c.TotalOtherIcms).
                HasColumnName(InvoiceConstants.FieldTotalOtherIcms).
                HasColumnType("money");

            builder.Property(c => c.TotalProducts).
                HasColumnName(InvoiceConstants.FieldTotalProducts).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.ShippingCost).
                HasColumnName(InvoiceConstants.FieldShippingCost).
                HasColumnType("money");

            builder.Property(c => c.InsuranceCost).
                HasColumnName(InvoiceConstants.FieldInsuranceCost).
                HasColumnType("money");

            builder.Property(c => c.TotalDiscont).
                HasColumnName(InvoiceConstants.FieldTotalDiscont).
                HasColumnType("money");

            builder.Property(c => c.OtherCosts).
                HasColumnName(InvoiceConstants.FieldOtherCosts).
                HasColumnType("money");

            builder.Property(c => c.IpiValue).
                HasColumnName(InvoiceConstants.FieldIpiValue).
                HasColumnType("money");

            builder.Property(c => c.TotalInvoice).
                HasColumnName(InvoiceConstants.FieldTotalInvoice).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.AdditionalInformation).
                HasColumnName(InvoiceConstants.FieldAdditionalInformation).
                HasMaxLength(1024).
                HasColumnType("varchar(1024)");

            builder.Property(c => c.ReservedFisco).
                HasColumnName(InvoiceConstants.FieldReservedFisco).
                HasMaxLength(1024).
                HasColumnType("varchar(1024)");

            builder.Property(c => c.OrderId).
                HasColumnName(InvoiceConstants.FieldOrderId).
                IsRequired(false);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);
            builder.Ignore(c => c.Company);
            builder.Ignore(c => c.Shipping);

            builder.HasMany(c => c.InvoiceLines).WithOne();
            builder.HasOne(c => c.Order).WithOne().OnDelete(DeleteBehavior.SetNull);

            builder.HasKey(c => c.Id);
            builder.ToTable(InvoiceConstants.TableInvoice);
        }
    }
}