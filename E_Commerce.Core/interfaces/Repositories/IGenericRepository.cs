using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications;

namespace E_Commerce.Core.interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {

        Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetWithSpecsAsync(ISpecifications<T> specifications);
        Task<int> GetCountWithSpecsAsync(ISpecifications<T> specifications);
        Task AddAsync(T entity);
        void Delete(T entity);


    }

}
