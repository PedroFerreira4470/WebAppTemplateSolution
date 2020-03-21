using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder) 
        {
            builder.Property(e => e.CustomerId).ValueGeneratedOnAdd();

            builder.Property(e => e.Address)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.CompanyName)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.ContactName)
                .IsRequired()
                .HasColumnType("nvarchar(20)");


            builder.Property(e => e.Country)
                .HasColumnType("varchar(20)");


            builder.Property(e => e.PostalCode)
                .HasColumnType("varchar(10)");

            //pass this two fields from the base entity to a generic confi to remove redundance
            //https://stackoverflow.com/questions/53275567/how-to-apply-common-configuration-to-all-entities-in-ef-core
            builder.Property(e => e.CreatedBy)
               .IsRequired()
               .HasColumnType("nvarchar(50)");
            builder.Property(e => e.LastModifiedBy)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
