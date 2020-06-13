using reg.Scaffolds.interfaces;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reg.Repository
{
    public abstract partial class RepositoryBase<T> : IRepositoryBase<T> where T: class, IScaffold
    {
        public virtual async Task<T> AddAsync(T schema) => (await _db.Set<T>().AddAsync(schema)).Entity;
        public virtual async Task AddRangeAsync(IEnumerable<T> schemas) => await _db.Set<T>().AddRangeAsync(schemas);

        public virtual void Delete(T schema) => _db.Set<T>().Remove(schema);
        public virtual void DeleteRange(IEnumerable<T> schemas) => _db.Set<T>().RemoveRange(schemas);

        public virtual void Update(T schema) => _db.Set<T>().Update(schema);
        public virtual void UpdateRange(IEnumerable<T> schemas) => _db.Set<T>().UpdateRange(schemas);

        public async Task SaveChangeAsync() => await _db.SaveChangesAsync();
    }
}
