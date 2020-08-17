
using Application.Common.Interfaces;
using Domain.Extensions.ShadowProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using System;

namespace Infrastructure.Persistance.EFFilterExtensions
{
    public static class SaveChangesFilter
    {

        public static void SaveChangesQueryFilters(ChangeTracker changeTracker, ICurrentUserService currentUserService)
        {
            if (currentUserService == null)
            {
                throw new ArgumentNullException(nameof(currentUserService));
            }

            if (changeTracker == null)
            {
                throw new ArgumentNullException(nameof(changeTracker));
            }

            foreach (var entry in changeTracker.Entries())
            {
                var entryTypes = entry.Entity.GetType();
                if (entryTypes.GetCustomAttributes(typeof(AuditableAttribute), true).Any())
                {
                    ConfigureAuditableEntity(entry, currentUserService);
                }

                if (entryTypes.GetCustomAttributes(typeof(SoftDeleteAttribute), true).Any())
                {
                    ConfigureActiveEntity(entry);
                }
            }
        }

        private static void ConfigureActiveEntity(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entry.Property("IsActive").CurrentValue = true;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Property("IsActive").CurrentValue = false;
                    //HandleRelationalEntities(entry, db);
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ConfigureAuditableEntity(EntityEntry entry, ICurrentUserService currentUserService)
        {
            var currentEmail = currentUserService.Email;

            switch (entry.State)
            {
                case EntityState.Added:
                    {
                        entry.Property("CreatedBy").CurrentValue = currentEmail;
                        entry.Property("Created").CurrentValue = DateTime.UtcNow;
                        entry.Property("LastModifiedBy").CurrentValue = currentEmail;
                        entry.Property("LastModified").CurrentValue = null;
                        break;
                    }
                case EntityState.Modified:
                case EntityState.Deleted:
                    {
                        entry.Property("LastModifiedBy").CurrentValue = currentEmail;
                        entry.Property("LastModified").CurrentValue = DateTime.UtcNow;
                        break;
                    }
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            };
        }




        //Active is active = false for the relational entities
        //private static void HandleRelationalEntities(EntityEntry entry, TemplateDbContext db) {

        //    foreach (var navigationEntry in entry.Navigations.Where(n => !n.Metadata.IsDependentToPrincipal()))
        //    {
        //        if (navigationEntry is CollectionEntry collectionEntry)
        //        {

        //            foreach (var dependentEntry in collectionEntry.CurrentValue)
        //            {
        //                navigationEntry.EntityEntry.State = EntityState.Modified;
        //                HandleDependent(db.Entry(dependentEntry));
        //            }
        //        }
        //        else
        //        {
        //            var dependentEntry = navigationEntry.CurrentValue;
        //            if (dependentEntry != null)
        //            {
        //                navigationEntry.EntityEntry.State = EntityState.Modified;
        //                HandleDependent(db.Entry(dependentEntry));
        //            }
        //        }
        //    }
        //}

        //private static void HandleDependent(EntityEntry entry)
        //{
        //    entry.CurrentValues["IsActive"] = false;
        //}

    }
}
