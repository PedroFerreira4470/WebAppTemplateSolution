using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.EntitiesConfiguration
{
    public class ValueConfiguration : IEntityTypeConfiguration<Value>
    {
        public void Configure(EntityTypeBuilder<Value> builder)
        {
            //pass this two fields from the base entity to a generic confi to remove redundance
            //https://stackoverflow.com/questions/53275567/how-to-apply-common-configuration-to-all-entities-in-ef-core
            builder.Property(e => e.CreatedBy)
               .IsRequired()
               .HasColumnType("nvarchar(50)");
            builder.Property(e => e.LastModifiedBy)
                .IsRequired()
                .HasColumnType("nvarchar(50)");
        }
    }
}
