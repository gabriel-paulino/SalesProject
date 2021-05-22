using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Constants.Database;
using SalesProject.Domain.Entities;

namespace SalesProject.Infra.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            builder.Property(u => u.Username).
                HasColumnName(UserConstants.FieldUsername).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired();

            builder.Property(u => u.Name).
                HasColumnName(UserConstants.FieldName).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired();

            builder.Property(u => u.Email).
                HasColumnName(UserConstants.FieldEmail).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired();

            builder.Property(u => u.PasswordHash).
                HasColumnName(UserConstants.FieldPasswordHash).
                HasMaxLength(250).
                HasColumnType("varchar(250)").
                IsRequired();

            builder.Property(u => u.Role).
                HasColumnName(UserConstants.FieldRole).
                HasConversion<int>();

            builder.Property(u => u.CustomerId).
                HasColumnName(UserConstants.FieldCustomerId).
                IsRequired(false);


            builder.Ignore(u => u.Notifications);
            builder.Ignore(u => u.Valid);

            builder.HasKey(u => u.Id);

            builder.ToTable(UserConstants.TableUser);
        }
    }
}