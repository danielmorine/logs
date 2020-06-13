using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace reg
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            this.ConnectionString = this.Database.GetDbConnection().ConnectionString;
        }

        public string ConnectionString { get; set; }
    }
}
