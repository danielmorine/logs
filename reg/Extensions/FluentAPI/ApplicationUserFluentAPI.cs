using Microsoft.EntityFrameworkCore;
using Scaffolds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reg.Extensions.FluentAPI
{
    public static class ApplicationUserFluentAPI
    {
        public static ModelBuilder ApplicationUserBuilder(this ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity => 
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).HasColumnType("VARCHAR(36)").HasMaxLength(36).IsRequired();
                entity.Property(x => x.CreatedDate).IsRequired();
                entity.Property(x => x.Email).IsRequired();
            });
            return builder;
        }
    }
}
