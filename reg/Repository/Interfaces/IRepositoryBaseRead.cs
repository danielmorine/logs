using Queries.Interfaces;
using reg.Scaffolds.interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryBaseRead<T> where T: class, IScaffold
    {
        Task<IEnumerable<T>> GetAllAsync(bool asNoTracking, int take = 10, int skip = 1);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> condiction, bool asNoTracking = false, int take = 10, int skip = 1, params string [] includes);
        Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, bool>> condiction,
            Expression<Func<T, U>> selection,
            bool asNoTracking = false,
            int take = 10,
            int skip = 1,
            params string[] includes) where U : IQuery;

        Task<IEnumerable<U>> GetAllAsync<U>(
            Expression<Func<T, bool>> condiction,
            Expression<Func<T, U>> selection,
            Expression<Func<T, string>> orderBy,
            bool desc = false,
            bool asNoTracing = false,
            int take = 10,
            int skip = 1,
            params string[] includes) where U: IQuery;

        Task<IEnumerable<U>> GetAllAsync<U, S>(
            Expression<Func<T, bool>> condiction,
            Expression<Func<T, U>> selection,
            Expression<Func<T, S>> orderBy,
            bool desc = false,
            bool asNoTracking = false,
            int take = 10,
            int skip =1,
            params string[] includes) where U : IQuery where S: struct;

        Task<T> FirstOrDefaultAsync(bool asNoTracking = false, params string[] includes);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, bool asNoTracking = false, params string[] includes);
        Task<T> LastOrDefault<U, S>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, U>> selection,
            Expression<Func<T, S>> orderBy,
            bool desc = false,
            bool asNoTracking = false,
            params string[] includes);
    }
}
