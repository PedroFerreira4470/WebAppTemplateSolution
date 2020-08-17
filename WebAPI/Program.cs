using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
#pragma warning disable IDE1006 // Naming Styles
        public static async Task Main(string[] args)
#pragma warning restore IDE1006 // Naming Styles
        {
            var conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(conf)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

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
                Console.WriteLine("Starting API....");
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error while starting the application");
            }
            finally
            {
                Log.CloseAndFlush();
            }
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

            Console.WriteLine($"Welcome To {nameof(WebAPI)}, have fun :)");
        }

    }
}
