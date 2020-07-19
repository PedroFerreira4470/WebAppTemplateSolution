using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace WebApplicationTemplate
{
    public class Program
    {
#pragma warning disable IDE1006 // Naming Styles
        public static async Task Main(string[] args)
#pragma warning restore IDE1006 // Naming Styles
        {

            var host = CreateHostBuilder(args)
                .Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            ConsoleLogInformation();
            try
            {
                var context = services.GetRequiredService<TemplateDbContext>();
                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                }
                var userManager = services.GetRequiredService<UserManager<User>>();
                
                await SeedData.SeedDefaultUsersAsync(userManager);
                await SeedData.SeedDataAsync(context);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }
            Console.WriteLine("Starting API....");
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                        .UseSerilog()
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        });
        }

        private static void ConsoleLogInformation()
        {
            Console.WriteLine(
            @"
---------------------------------------------------
-----*******************************************---
-----***************TemplateName****************---
-----*******************************************---
---------------------------------------------------"
            );

            Console.WriteLine($"Welcome To {nameof(WebApplicationTemplate)}, have fun :)");
        }

    }
}
