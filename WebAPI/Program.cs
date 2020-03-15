using System;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistance;
using Persistance.Seed;
using Serilog;

namespace WebApplicationTemplate
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            ConsoleLogInformation();
            try
            {
                var context = services.GetRequiredService<TemplateDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                await context.Database.MigrateAsync();
                await SeedData.SeedDataAsync(context,userManager);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }
            Console.WriteLine($"Opening APP....");
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

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
            Console.WriteLine($"Welcome To {nameof(WebApplicationTemplate)}");
        }       

    }
}
