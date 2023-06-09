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
    internal class UserAddressConfiguration : EntityConfiguration<UserAddress>
    {
        protected override void ConfigureRules(EntityTypeBuilder<UserAddress> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Number).IsRequired();

            builder.HasIndex(x => x.Name);

            builder.HasOne(x => x.User).WithOne(x => x.Address).HasForeignKey<UserAddress>(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
