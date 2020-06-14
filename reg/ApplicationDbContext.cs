using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using reg.Extensions.FluentAPI;
using Repository.Interfaces;
using Scaffolds;

namespace reg
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            this.ConnectionString = this.Database.GetDbConnection().ConnectionString;
        }

        public string ConnectionString { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplicationUserBuilder();

            base.OnModelCreating(builder);
        }
    }
}
