using E_Commerce.Core.interfaces.Repositories;
using E_Commerce.Core.Models;

namespace E_Commerce.Core
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;
        Task<int> CompleteAsync();
    }
}
