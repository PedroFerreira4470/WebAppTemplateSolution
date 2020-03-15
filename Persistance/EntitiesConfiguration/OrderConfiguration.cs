using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.OrderId).ValueGeneratedOnAdd();

            builder.Property(e => e.OrderName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Priority)
                .HasColumnType("tinyint")
                .IsRequired();

            //pass this two fields from the base entity to a generic confi to remove redundance
            //https://stackoverflow.com/questions/53275567/how-to-apply-common-configuration-to-all-entities-in-ef-core
            builder.Property(e => e.CreatedBy)
               .IsRequired()
               .HasColumnType("nvarchar(50)");
            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            //Example one to many
            //builder.HasOne(d => d.Shipper)
            //    .WithMany(p => p.Orders)
            //    .HasForeignKey(d => d.ShipVia)
            //    .HasConstraintName("FK_Orders_Shippers");
        }
    }
}
