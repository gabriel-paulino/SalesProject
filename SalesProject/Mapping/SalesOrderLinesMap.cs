﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class SalesOrderLinesMap : IEntityTypeConfiguration<SalesOrderLines>
    {
        public void Configure(EntityTypeBuilder<SalesOrderLines> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.Quantity).
               HasColumnName(SalesOrderLinesConstants.FieldQuantity).
               IsRequired();

            builder.Property(c => c.UnitaryPrice).
                HasColumnName(SalesOrderLinesConstants.FieldUnitaryPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.TotalPrice).
                HasColumnName(SalesOrderLinesConstants.FieldTotalPrice).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.PercentageUnitaryDiscont).
                HasColumnName(SalesOrderLinesConstants.FieldPercentageUnitaryDiscont);

            builder.Property(c => c.ValueUnitaryDiscont).
                HasColumnName(SalesOrderLinesConstants.FieldValueUnitaryDiscont).
                HasColumnType("money");

            builder.Property(c => c.AdditionalCosts).
                HasColumnName(SalesOrderLinesConstants.FieldAdditionalCosts).
                HasColumnType("money");

            builder.Property(c => c.Cst).
                HasColumnName(SalesOrderLinesConstants.FieldCst).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(c => c.Cfop).
                HasColumnName(SalesOrderLinesConstants.FieldCfop).
                HasMaxLength(30).
                HasColumnType("varchar(30)").
                IsRequired();

            builder.Property(c => c.BaseCalcIcms).
                HasColumnName(SalesOrderLinesConstants.FieldBaseCalcIcms).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IcmsValue).
                HasColumnName(SalesOrderLinesConstants.FieldIcmsValue).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IpiValue).
                HasColumnName(SalesOrderLinesConstants.FieldIpiValue).
                HasColumnType("money").
                IsRequired();

            builder.Property(c => c.IcmsAliquot).
                HasColumnName(SalesOrderLinesConstants.FieldIcmsAliquot).
                IsRequired();

            builder.Property(c => c.IpiAliquot).
                HasColumnName(SalesOrderLinesConstants.FieldIpiAliquot).
                IsRequired();

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);
            builder.Ignore(c => c.TaxLine);

            builder.HasKey(c => c.Id);
            builder.ToTable(SalesOrderLinesConstants.TableSalesOrderLines);
        }
    }
}