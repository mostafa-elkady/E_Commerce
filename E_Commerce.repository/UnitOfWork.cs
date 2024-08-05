using E_Commerce.Core;
using E_Commerce.Core.interfaces.Repositories;
using E_Commerce.Core.Models;
using E_Commerce.repository.Data;
using System.Collections;

namespace E_Commerce.repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();


        public async ValueTask DisposeAsync() => await _context.DisposeAsync();


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var Repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, Repository);
            }
            return (_repositories[type] as IGenericRepository<TEntity>)!;

        }
    }
}
