using reg.Scaffolds.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryBaseWrite<T> where T: class, IScaffold
    {
        Task<T> AddAsync(T schema);
        Task AddRangeAsync(IEnumerable<T> schemas);

        void Delete(T schema);
        void DeleteRange(IEnumerable<T> schemas);

        void Update(T schema);
        void UpdateRange(IEnumerable<T> schemas);

        Task SaveChangeAsync();
    }
}
