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
    public class SideConfiguration : EntityConfiguration<Side>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Side> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.RestaurantFoodCategories).WithOne(x => x.Side).HasForeignKey(x => x.SideId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
