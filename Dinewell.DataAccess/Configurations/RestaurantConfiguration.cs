using Dinewell.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.DataAccess.Configurations
{
    public class RestaurantConfiguration : EntityConfiguration<Restaurant>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.AddressNumber).IsRequired();
            builder.Property(x => x.WorkFrom).IsRequired();
            builder.Property(x => x.WorkTo).IsRequired();

            builder.HasIndex(x => x.Name);
        }
    }
}
