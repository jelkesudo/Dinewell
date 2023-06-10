using Dinewell.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.DataAccess.Configurations
{
    public class RestaurantFoodCategoryConfiguration : EntityConfiguration<RestaurantFoodCategory>
    {
        protected override void ConfigureRules(EntityTypeBuilder<RestaurantFoodCategory> builder)
        {
            
        }
    }
}
