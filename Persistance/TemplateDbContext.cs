using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistance.EFFilterExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Persistance
{
    public class TemplateDbContext : IdentityDbContext<User>
    {

        public TemplateDbContext(DbContextOptions options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Value> Values { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveChangesFilter.SaveChangesQueryFilters(this.ChangeTracker);
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SaveChangesFilter.SaveChangesQueryFilters(this.ChangeTracker);
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
