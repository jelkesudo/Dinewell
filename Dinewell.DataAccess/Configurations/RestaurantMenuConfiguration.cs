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
    public class RestaurantMenuConfiguration : EntityConfiguration<RestaurantMenu>
    {
        protected override void ConfigureRules(EntityTypeBuilder<RestaurantMenu> builder)
        {
            builder.Property(x => x.Description).IsRequired();

            builder.HasMany(x => x.OrderMeals).WithOne(x => x.Meal).HasForeignKey(x => x.MealId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
