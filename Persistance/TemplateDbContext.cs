using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistance.EFFilterExtensions;
using System;
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


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesFilter.SaveChangesQueryFilters(this.ChangeTracker,this);
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SaveChangesFilter.SaveChangesQueryFilters(this.ChangeTracker, this);
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



  //foreach (var entry in ChangeTracker.Entries())
  //          {
  //              //.Entries<IAuditable>()
  //              //var tracked = entity.Entity as IWhen;
  //              if (entry.GetType() == typeof(IAuditable))
  //              {
  //                  var auditableEntry = (IAuditable)entry.Entity;
  //                  switch (entry.State)
  //                  {
  //                      case EntityState.Added:
  //                          auditableEntry.CreatedBy = 1; //TODO
  //                          auditableEntry.Created = DateTime.UtcNow;
  //                          break;
  //                      case EntityState.Modified:
  //                          auditableEntry.LastModifiedBy = 1; //TODO
  //                          auditableEntry.LastModified = DateTime.UtcNow;
  //                          break;
  //                  };
  //              }

  //              if (entry.GetType() == typeof(IActive))
  //              {
  //                  var activeEntry = (IActive)entry.Entity;
  //                  switch (entry.State)
  //                  {
  //                      case EntityState.Added:
  //                          activeEntry.IsActive = true;
  //                          break;
  //                      case EntityState.Modified:
  //                          activeEntry.IsActive = true;
  //                          break;
  //                      case EntityState.Deleted:
  //                          entry.State = EntityState.Modified;
  //                          activeEntry.IsActive = false;
  //                          break;
  //                  }
  //              }
  //          }