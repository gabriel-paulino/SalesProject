using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(c => c.FirstName).
                HasColumnName(ContactConstants.FieldFirstName).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Property(c => c.LastName).
                HasColumnName(ContactConstants.FieldLastName).
                HasMaxLength(20).
                HasColumnType("varchar(20)");

            builder.Property(c => c.FullName).
                HasColumnName(ContactConstants.FieldFullName).
                HasMaxLength(40).
                HasColumnType("varchar(40)");

            builder.Property(c => c.Email).
                HasColumnName(ContactConstants.FieldEmail).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired();

            builder.Property(c => c.WhatsApp).
                HasColumnName(ContactConstants.FieldWhatsApp).
                HasMaxLength(20).
                HasColumnType("varchar(20)");

            builder.Property(c => c.Phone).
                HasColumnName(ContactConstants.FieldPhone).
                HasMaxLength(20).
                HasColumnType("varchar(20)").
                IsRequired();

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);

            builder.HasKey(c => c.Id);
            builder.ToTable(ContactConstants.TableContact);
        }
    }
}
