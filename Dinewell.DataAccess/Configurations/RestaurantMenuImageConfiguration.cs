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
    public class RestaurantMenuImageConfiguration : EntityConfiguration<RestaurantMenuImage>
    {
        protected override void ConfigureRules(EntityTypeBuilder<RestaurantMenuImage> builder)
        {
            builder.Property(x => x.ImageName).IsRequired();
            builder.Property(x => x.ImageAlt).IsRequired();
            builder.Property(x => x.ImageExtension).IsRequired();
            builder.Property(x => x.ImageSize).IsRequired();
            builder.Property(x => x.ImageData).IsRequired();
            builder.Property(x => x.RestaurantMenuId).IsRequired();

            builder.HasOne(x => x.RestaurantMenu).WithOne(x => x.RestaurantMenuImage).HasForeignKey<RestaurantMenuImage>(x => x.RestaurantMenuId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
