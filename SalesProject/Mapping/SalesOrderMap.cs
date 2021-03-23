﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class SalesOrderMap : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasKey(c => c.Id);
            builder.ToTable(SalesOrderConstants.TableSalesOrder);
        }
    }
}