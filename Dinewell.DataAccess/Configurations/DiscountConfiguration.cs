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
    public class DiscountConfiguration : EntityConfiguration<Discount>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.DateFrom).IsRequired();

            builder.HasOne(x => x.Food).WithMany(x => x.Discounts).HasForeignKey(x => x.FoodId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
