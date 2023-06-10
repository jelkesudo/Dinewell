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
    public class SidePriceConfiguration : EntityConfiguration<SidePrice>
    {
        protected override void ConfigureRules(EntityTypeBuilder<SidePrice> builder)
        {
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.PriceDate).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.RestaurantSide).WithMany(x => x.SidePrices).HasForeignKey(x => x.SideId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
