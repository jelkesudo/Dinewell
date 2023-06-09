using Dinewell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.DataAccess.Configurations
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);
            builder.Property(x => x.DeletedBy).HasMaxLength(50);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            ConfigureRules(builder);
        }
        protected abstract void ConfigureRules(EntityTypeBuilder<T> builder);
    }
}
