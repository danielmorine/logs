using reg.Scaffolds;
using Repository.Interfaces;

namespace reg.Repository
{
    public interface IEnvironmentTypeRepository : IRepositoryBase<EnvironmentType> { }

    public class EnvironmentTypeRepository : RepositoryBase<EnvironmentType>, IEnvironmentTypeRepository
    {
        public EnvironmentTypeRepository(IApplicationDbContext db) : base(db) { }
    }
}
