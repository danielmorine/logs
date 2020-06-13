using Microsoft.EntityFrameworkCore;
using Queries.Interfaces;
using reg.Scaffolds.interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace reg.Repository
{
    public abstract partial class RepositoryBase<T> : IRepositoryBase<T> where T : class, IScaffold
    {
        public async Task<T> FirstOrDefaultAsync(bool asNoTracking = false, params string[] includes)
        {
            var query = _db.Set<T>().AsQueryable();
            if (asNoTracking)
            {
                query = _db.Set<T>().AsNoTracking().AsQueryable();
            }

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                    query = query.Include(includes[i]);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, bool asNoTracking = false, params string[] includes)
        {
            var query = _db.Set<T>().AsQueryable();
            if (asNoTracking)
            {
                query = _db.Set<T>().AsNoTracking().AsQueryable();
            }

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                    query = query.Include(includes[i]);
            }

            if (predicate != null)
            {
                return await query.FirstOrDefaultAsync(predicate);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking, int take = 10, int skip = 1)
        {
            var query = _db.Set<T>().AsQueryable();
            if (asNoTracking)
            {
                query = _db.Set<T>().AsNoTracking().AsQueryable();
            }

            skip = take * (skip - 1);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> condiction, bool asNoTracking = false, int take = 10, int skip = 1, params string[] includes)
        {
            var query = _db.Set<T>().AsQueryable();
            if (asNoTracking)
            {
                query = _db.Set<T>().AsNoTracking().AsQueryable();
            }

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            skip = take * (skip - 1);
            if (condiction == null)
            {
                return await query.Skip(skip).Take(take).ToListAsync();
            }

            return await query.Where(condiction).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, bool>> condiction, Expression<Func<T, U>> selection, bool asNoTracking = false, int take = 10, int skip = 1, params string[] includes) where U : IQuery
        {
            var query = _db.Set<T>().AsQueryable();

            if (asNoTracking)
            {
                query = _db.Set<T>().AsNoTracking().AsQueryable();
            }

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                    query = query.Include(includes[i]);
            }

            var calcSkip = (take * (skip - 1));
            if (condiction == null)
            {
                return await query
                    .Select(selection)
                    .Skip(calcSkip)
                    .Take(take)
                    .ToListAsync();
            }

            return await query.Where(condiction)
                .Select(selection)
                .Skip(calcSkip)
                .Take(take)
                .ToListAsync();
        }

        public Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, bool>> condiction, Expression<Func<T, U>> selection, Expression<Func<T, string>> orderBy, bool desc = false, bool asNoTracing = false, int take = 10, int skip = 1, params string[] includes) where U : IQuery
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<U>> GetAllAsync<U, S>(Expression<Func<T, bool>> condiction, Expression<Func<T, U>> selection, Expression<Func<T, S>> orderBy, bool desc = false, bool asNoTracking = false, int take = 10, int skip = 1, params string[] includes)
            where U : IQuery
            where S : struct
        {
            throw new NotImplementedException();
        }

        public Task<T> LastOrDefault<U, S>(Expression<Func<T, bool>> predicate, Expression<Func<T, U>> selection, Expression<Func<T, S>> orderBy, bool desc = false, bool asNoTracking = false, params string[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
