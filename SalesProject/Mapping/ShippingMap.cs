using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesProject.Domain.Entities;

namespace SalesProject.Mapping
{
    public class ShippingMap : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.Property(c => c.Id).
                ValueGeneratedOnAdd().
                HasColumnType("uniqueidentifier");

            /*
             * 
             * Não está sendo usado
             * 
             * */

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Valid);
        }
    }
}