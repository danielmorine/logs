using Repository.Interfaces;
using Scaffolds;

namespace reg.Repository
{
    public interface IApplicationUserRepository : IRepositoryBase<ApplicationUser> { }

    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>
    {
        public ApplicationUserRepository(IApplicationDbContext db): base(db) { }
    }
}
