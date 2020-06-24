using Application.Common.Interfaces;
using Dapper;
using Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Infrastructure.Persistance.EFFilterExtensions;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class TemplateDbContext : ApiAuthorizationDbContext<User>, ITemplateDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public TemplateDbContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService)
            : base(options, operationalStoreOptions)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            _currentUserService = currentUserService;
            //DbConnection = this.Database.GetDbConnection();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Value> Values { get; set; }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection dbConnection = this.Database.GetDbConnection();
            return await dbConnection
                .QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveChangesFilter.SaveChangesQueryFilters(this.ChangeTracker, _currentUserService);
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SaveChangesFilter.SaveChangesQueryFilters(this.ChangeTracker, _currentUserService);
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsActiveQueryFilter();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TemplateDbContext).Assembly);

            //This was made for simplicity, the "correct" way is creating for each Identity"_" class a Configuration class,
            //in the folder EntitiesConfigurations
            IdentityConfigurations(modelBuilder);


        }

        private void IdentityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}
