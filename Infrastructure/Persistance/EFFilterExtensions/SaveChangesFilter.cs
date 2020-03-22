
using Application.Common.Interfaces;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Infrastructure.Persistance.EFFilterExtensions
{
    public static class SaveChangesFilter
    {

        public static void SaveChangesQueryFilters(ChangeTracker ChangeTracker, ICurrentUserService currentUserService)
        {

            //TODO Put in one foreach instead of two
            foreach (var entry in ChangeTracker.Entries<Auditable>())
            {
                var currentEmail = currentUserService.GetEmail();
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentEmail;
                        entry.Entity.Created = DateTime.UtcNow;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = currentEmail;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = currentEmail;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.LastModifiedBy = currentEmail;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                };
            }


            foreach (var entry in ChangeTracker.Entries<IActive>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IsActive = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.IsActive = true;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsActive = false;
                        //HandleRelationalEntities(entry, db);
                        break;
                }
            }



        }


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
