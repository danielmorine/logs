using reg.Scaffolds;
using Repository.Interfaces;

namespace reg.Repository
{
    public interface IRegistrationProcessRepository : IRepositoryBase<RegistrationProcess> { }
    public class RegistrationProcessRepository : RepositoryBase<RegistrationProcess>, IRegistrationProcessRepository
    {
        public RegistrationProcessRepository(IApplicationDbContext db) : base(db) { }
    }
}
