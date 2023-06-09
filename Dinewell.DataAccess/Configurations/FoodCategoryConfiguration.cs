using Dinewell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.DataAccess.Configurations
{
    public class FoodCategoryConfiguration : EntityConfiguration<FoodCategory>
    {
        protected override void ConfigureRules(EntityTypeBuilder<FoodCategory> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Restaurants).WithOne(x => x.FoodCategory).HasForeignKey(x => x.FoodCategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
