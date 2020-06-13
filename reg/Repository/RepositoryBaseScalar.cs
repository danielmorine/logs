using Microsoft.EntityFrameworkCore;
using reg.Scaffolds.interfaces;
using Repository.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace reg.Repository
{
    public abstract partial class RepositoryBase<T> : IRepositoryBase<T> where T : class, IScaffold
    {
        public virtual async Task<bool> AnyAsync() => await _db.Set<T>().AnyAsync();
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await _db.Set<T>().AsNoTracking().AnyAsync(predicate);
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _db.Set<T>().AsQueryable();
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                    query = query.Include(includes[i]);
            }

            if (predicate != null)
            {
                return await query.AnyAsync(predicate);
            }

            return await query.AnyAsync();
        }

        public virtual async Task<int> CountAsync() => await _db.Set<T>().CountAsync();
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {

            if (predicate == null)
            {
                return await _db.Set<T>().CountAsync();
            }

            return await _db.Set<T>().CountAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _db.Set<T>().AsQueryable();
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                    query = query.Include(includes[i]);
            }

            if (predicate != null)
            {
                return await query.CountAsync(predicate);
            }

            return await query.CountAsync();
        }
    }
}
