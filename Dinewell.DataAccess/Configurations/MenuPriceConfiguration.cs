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
    public class MenuPriceConfiguration : EntityConfiguration<MenuPrice>
    {
        protected override void ConfigureRules(EntityTypeBuilder<MenuPrice> builder)
        {
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.PriceDate).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Food).WithMany(x => x.MenuPrices).HasForeignKey(x => x.FoodId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
