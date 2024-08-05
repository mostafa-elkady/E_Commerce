using E_Commerce.Core.interfaces.Repositories;
using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications;
using E_Commerce.repository.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context) // ASk The CLR To Create Object from Store Context To Open Connection With Database
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();

        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);

        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).ToListAsync();
        }

        public async Task<T> GetWithSpecsAsync(ISpecifications<T> specifications)
        {
            return (await ApplySpecifications(specifications).FirstOrDefaultAsync())!;
        }

        public async Task<int> GetCountWithSpecsAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).CountAsync();
        }
        private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
        {
            return SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specifications);
        }

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);


        public async void Delete(T entity) => _context.Set<T>().Remove(entity);

    }
}
