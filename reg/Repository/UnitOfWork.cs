using Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace reg.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private bool _disposed = false;

        public UnitOfWork(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CommitAsync()
        {
            if (_disposed) throw new ObjectDisposedException(this.GetType().FullName);
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _applicationDbContext != null)
            {
                _applicationDbContext.Dispose();
            }

            _disposed = true;
        }
    }
}
