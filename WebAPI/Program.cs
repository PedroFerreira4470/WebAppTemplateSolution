using System;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistance;
using Persistance.Seed;
using Serilog;

namespace WebApplicationTemplate
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
    
            try
            {

                var host = CreateHostBuilder(args)
                    .Build();

                ConsoleLogInformation();
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<TemplateDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                await context.Database.MigrateAsync();
                await SeedData.SeedDataAsync(context,userManager);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to Start correctly!");
            }
            finally {
                Log.CloseAndFlush();
            }
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
            Log.Information("APP Starting: Welcome To TemplateName");
        }       

    }
}
