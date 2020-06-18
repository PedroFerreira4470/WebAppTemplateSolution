
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
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Auditable auditableEntity)
                    ConfigureAuditableEntity(entry, auditableEntity, currentUserService);

                if (entry.Entity is IActive activeEntity)
                    ConfigureActiveEntity(entry, activeEntity); 
            }
        }

        private static void ConfigureActiveEntity(EntityEntry entry, IActive activeEntity)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    activeEntity.IsActive = true;
                    break;
                case EntityState.Modified:
                    activeEntity.IsActive = true;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    activeEntity.IsActive = false;
                    //HandleRelationalEntities(entry, db);
                    break;
            }
        }

        private static void ConfigureAuditableEntity(EntityEntry entry, Auditable auditableEntity, ICurrentUserService currentUserService)
        {
            var currentEmail = currentUserService.GetEmail();

            switch (entry.State)
            {
                case EntityState.Added:
                    auditableEntity.CreatedBy = currentEmail;
                    auditableEntity.Created = DateTime.UtcNow;
                    auditableEntity.LastModified = null;
                    auditableEntity.LastModifiedBy = currentEmail;
                    break;
                case EntityState.Modified:
                    auditableEntity.LastModifiedBy = currentEmail;
                    auditableEntity.LastModified = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    auditableEntity.LastModifiedBy = currentEmail;
                    auditableEntity.LastModified = DateTime.UtcNow;
                    break;
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
