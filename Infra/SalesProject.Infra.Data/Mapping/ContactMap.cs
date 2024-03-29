﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier")
                .HasComment(ContactConstants.Id);

            builder.Property(c => c.FirstName).
                HasColumnName(ContactConstants.FieldFirstName).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired().
                HasComment(ContactConstants.FirstName);

            builder.Property(c => c.LastName).
                HasColumnName(ContactConstants.FieldLastName).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                HasComment(ContactConstants.LastName);

            builder.Property(c => c.FullName).
                HasColumnName(ContactConstants.FieldFullName).
                HasMaxLength(40).
                HasColumnType("varchar(40)").
                HasComment(ContactConstants.FullName);

            builder.Property(c => c.Email).
                HasColumnName(ContactConstants.FieldEmail).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired().
                HasComment(ContactConstants.Email);

            builder.Property(c => c.WhatsApp).
                HasColumnName(ContactConstants.FieldWhatsApp).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                HasComment(ContactConstants.WhatsApp);

            builder.Property(c => c.Phone).
                HasColumnName(ContactConstants.FieldPhone).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired().
                HasComment(ContactConstants.Phone);

            builder.Property(c => c.CustomerId).
                HasColumnName(ContactConstants.FieldCustomerId).
                HasComment(ContactConstants.CustomerId);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasKey(c => c.Id);
            builder.ToTable(ContactConstants.TableContact);
        }
    }
}
