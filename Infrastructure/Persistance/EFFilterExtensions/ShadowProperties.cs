using System;
using System.Collections.Generic;
using System.Text;
using Domain.Extensions;
using Domain.Extensions.ShadowProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Persistance.EFFilterExtensions
{
    public static class ShadowProperties
    {
        public static void ApplyShadowProperties(this ModelBuilder modelBuilder, ITypeBase entityType)
        {
            modelBuilder.SetAuditableEntities(entityType); 
            modelBuilder.SetSoftDeleteEntities(entityType);

        }

        private static void SetAuditableEntities(this ModelBuilder modelBuilder, ITypeBase entityType)
        {
            if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Any())
            {
                modelBuilder
                    .Entity(entityType.Name)
                    .Property<string>("CreatedBy")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                modelBuilder
                    .Entity(entityType.Name)
                    .Property<string>("LastModifiedBy")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                modelBuilder
                    .Entity(entityType.Name)
                    .Property<DateTime>("Created");

                modelBuilder
                    .Entity(entityType.Name)
                    .Property<DateTime?>("LastModified");
            }
        }

        private static void SetSoftDeleteEntities(this ModelBuilder modelBuilder, ITypeBase entityType)
        {
            if (entityType.ClrType.GetCustomAttributes(typeof(SoftDeleteAttribute), true).Any())
            {
                modelBuilder
                    .Entity(entityType.Name)
                    .Property<bool>("IsActive")
                    .HasDefaultValue(0);

            }

        }
    }
}
