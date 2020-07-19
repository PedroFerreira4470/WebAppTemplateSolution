using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Domain.Extensions.ShadowProperties;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Infrastructure.Persistance.EFFilterExtensions
{
    public static class ActiveQueryFilter
    {
        private static readonly MethodInfo _setSoftDeleteFilterMethod = 
            typeof(ActiveQueryFilter)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetSoftDeleteFilter));

        public static void ApplyConfigurationActiveQueryFilter(this ModelBuilder modelBuilder, ITypeBase entityType)
        {
            if (entityType.ClrType.GetCustomAttributes(typeof(SoftDeleteAttribute), true).Any())
            {
                modelBuilder.SetSoftDeleteFilter(entityType.ClrType);
            }
        }

        private static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            _setSoftDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, new()
        {
            modelBuilder
                .Entity<TEntity>()
                .HasQueryFilter(e => EF.Property<bool>(e, "IsActive"));
        }

    }
}
