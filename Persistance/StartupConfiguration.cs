using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance
{
    public static class StartupConfiguration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<TemplateDbContext>(options => 
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(nameof(Persistance)))
                );

            return services;
            
        }

    }
}
