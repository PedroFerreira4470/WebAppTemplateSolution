using System;
using System.Collections.Generic;
using System.Text;
using Domain.Extensions.ShadowProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Persistance.EFFilterExtensions
{
    public static class CustomConfigurations
    {

        public static void ApplyCustomConfigurations(this ModelBuilder modelBuilder)
        {

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {

                modelBuilder.ApplyShadowProperties(entityType);
                modelBuilder.ApplyConfigurationActiveQueryFilter(entityType);
            }
        }
    }
}
