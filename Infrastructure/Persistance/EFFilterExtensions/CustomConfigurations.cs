using Microsoft.EntityFrameworkCore;

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
