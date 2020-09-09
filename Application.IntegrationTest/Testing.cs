using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI;

namespace Application.IntegrationTests
{

    [SetUpFixture]
    public class Testing
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        private static string _currentUserId;
        private static string _currentUserEmail;
        private static string _currentUsername;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingstests.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "WebAPI"));

            var startup = new Startup(_configuration);

            startup.ConfigureServices(services);

            // Replace service registration for ICurrentUserService
            // Remove existing registration
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            // Register testing version
            services.AddTransient(provider =>
                Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId && s.UserName == _currentUsername && s.Email == _currentUserEmail));

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };

            EnsureDatabase();
        }

        public void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<TemplateDbContext>();

            context.Database.Migrate();
        }

        public static async Task ResetStateAsync()
        {
            var conn = _configuration.GetConnectionString("DefaultConnection");
            await _checkpoint.Reset(conn);
            _currentUserId = null;
            _currentUsername = null;
            _currentUserEmail = null;
        }

        public static async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<TemplateDbContext>();

            return await context.FindAsync<TEntity>(id);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<TemplateDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<string> RunAsDefaultUserAsync()
        {
            return await RunAsUserAsync("test@local.com", "Passw0rd!");
        }

        public static async Task<string> RunAsUserAsync(string userName, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

            var user = new User { UserName = userName, Email = userName, DisplayName = userName };

            var result = await userManager.CreateAsync(user, password);

            _currentUserId = user.Id;
            _currentUsername = user.UserName;
            _currentUserEmail = user.Email;

            return _currentUserId;
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }
    }
}
