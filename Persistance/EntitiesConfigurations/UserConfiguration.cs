using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntitiesConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(e => e.Id).HasColumnName("UserId");

            builder.Property(e => e.DisplayName)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.Bio)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.UserName)
               .IsRequired()
               .HasColumnType("nvarchar(100)");
        }
    }
}
