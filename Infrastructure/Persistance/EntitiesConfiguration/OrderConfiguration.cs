using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
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


            //Example one to many
            //builder.HasOne(d => d.Shipper)
            //    .WithMany(p => p.Orders)
            //    .HasForeignKey(d => d.ShipVia)
            //    .HasConstraintName("FK_Orders_Shippers");
        }
    }
}
