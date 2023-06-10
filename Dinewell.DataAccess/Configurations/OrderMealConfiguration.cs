using Dinewell.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.DataAccess.Configurations
{
    public class OrderMealConfiguration : EntityConfiguration<OrderMeal>
    {
        protected override void ConfigureRules(EntityTypeBuilder<OrderMeal> builder)
        {
            
        }
    }
}
