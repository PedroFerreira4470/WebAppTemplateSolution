using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;


namespace Infrastructure.Persistance.EFFilterExtensions
{
    public static class ActiveQueryFilter
    {
        public static void ApplyConfigurationsActiveQueryFilter(this ModelBuilder modelBuilder)
        {

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IActive).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
        }

        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(ActiveQueryFilter)
                   .GetMethods(BindingFlags.Public | BindingFlags.Static)
                   .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, IActive
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => x.IsActive == true);
        }

    }
}
