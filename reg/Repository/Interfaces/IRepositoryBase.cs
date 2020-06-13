using reg.Scaffolds.interfaces;

namespace Repository.Interfaces
{
    public interface IRepositoryBase<T> :
        IRepositoryBaseRead<T>,
        IRepositoryBaseScalar<T>,
        IRepositoryBaseWrite<T> where T : class, IScaffold
    { }
}
