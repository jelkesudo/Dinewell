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
    public class RestaurantImageConfiguration : EntityConfiguration<RestaurantImage>
    {
        protected override void ConfigureRules(EntityTypeBuilder<RestaurantImage> builder)
        {
            builder.Property(x => x.ImageName).IsRequired();
            builder.Property(x => x.ImageAlt).IsRequired();
            builder.Property(x => x.ImageExtension).IsRequired();
            builder.Property(x => x.ImageSize).IsRequired();
            builder.Property(x => x.ImageData).IsRequired();
            builder.Property(x => x.RestaurantId).IsRequired();

            builder.HasOne(x => x.Restaurant).WithOne(x => x.RestaurantImage).HasForeignKey<RestaurantImage>(x => x.RestaurantId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
