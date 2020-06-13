
using reg.Scaffolds.interfaces;
using Repository.Interfaces;
using System;

namespace reg.Repository
{
    public abstract partial class RepositoryBase<T> : IRepositoryBase<T> where T: class, IScaffold
    {
        protected readonly IApplicationDbContext _db;

        private bool disposed = false;

        public RepositoryBase(IApplicationDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    if (_db != null)
                        _db.Dispose();

            disposed = true;
        }
    }
}
