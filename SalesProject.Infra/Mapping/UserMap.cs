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
                HasColumnType("uniqueidentifier").
                HasComment(UserConstants.Id);

            builder.Property(u => u.Username).
                HasColumnName(UserConstants.FieldUsername).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired().
                HasComment(UserConstants.Username);

            builder.Property(u => u.Name).
                HasColumnName(UserConstants.FieldName).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired().
                HasComment(UserConstants.Name);

            builder.Property(u => u.Email).
                HasColumnName(UserConstants.FieldEmail).
                HasMaxLength(50).
                HasColumnType("varchar(50)").
                IsRequired().
                HasComment(UserConstants.Email);

            builder.Property(u => u.PasswordHash).
                HasColumnName(UserConstants.FieldPasswordHash).
                HasMaxLength(250).
                HasColumnType("varchar(250)").
                IsRequired().
                HasComment(UserConstants.PasswordHash);

            builder.Property(u => u.Role).
                HasColumnName(UserConstants.FieldRole).
                HasConversion<int>().
                HasComment(UserConstants.Role);

            builder.Property(u => u.CustomerId).
                HasColumnName(UserConstants.FieldCustomerId).
                IsRequired(false).
                HasComment(UserConstants.CustomerId);

            builder.Ignore(u => u.Notifications);
            builder.Ignore(u => u.Valid);

            builder.HasKey(u => u.Id);

            builder.ToTable(UserConstants.TableUser);
        }
    }
}