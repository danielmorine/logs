using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Scaffolds;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> ApplicationUser { get; set; }
        string ConnectionString { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
        DbSet<T> Set<T>() where T : class;
        void Dispose();
    }
}
