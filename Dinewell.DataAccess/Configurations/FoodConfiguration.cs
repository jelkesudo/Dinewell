using Dinewell.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.DataAccess.Configurations
{
    public class FoodConfiguration : EntityConfiguration<Food>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Food> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Menus).WithOne(x => x.Food).HasForeignKey(x => x.FoodId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
