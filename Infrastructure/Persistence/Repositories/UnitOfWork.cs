using Domain.Contracts;
using Persistence.Data;
using System.Collections.Concurrent;

namespace Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _dbcontext;
        private ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
            => (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, TKey>(_dbcontext));
        public async Task<int> SaveChangesAsync()
            => await _dbcontext.SaveChangesAsync();
    }
}
