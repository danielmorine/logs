using reg.Scaffolds;
using Repository.Interfaces;

namespace reg.Repository
{
    public interface IReportRepository : IRepositoryBase<Report> { }
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(IApplicationDbContext db) : base(db) { }
    }
}
