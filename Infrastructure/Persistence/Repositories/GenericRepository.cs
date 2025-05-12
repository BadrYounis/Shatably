using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private readonly AppDbContext _dbcontext;
        public GenericRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
            => asNoTracking ?
            await _dbcontext.Set<TEntity>().ToListAsync() :
            await _dbcontext.Set<TEntity>().AsNoTracking().ToListAsync();
        public async Task<TEntity?> GetByIdAsync(Guid id)
            => await _dbcontext.Set<TEntity>().FindAsync(id);
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbcontext.Set<TEntity>().Where(predicate).ToListAsync();
        public async Task AddAsync(TEntity entity)
            => await _dbcontext.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity)
            => _dbcontext.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity)
            => _dbcontext.Set<TEntity>().Remove(entity);



        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
            => await ApplySpecifications(specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Specifications<TEntity> specifications)
            => await ApplySpecifications(specifications).FirstOrDefaultAsync();
        public async Task<int> CountAsync(Specifications<TEntity> specifications)
            => await ApplySpecifications(specifications).CountAsync();

        private IQueryable<TEntity> ApplySpecifications(Specifications<TEntity> specifications)
           => SpecificationEvaluator.GetQuery<TEntity>(_dbcontext.Set<TEntity>(), specifications);
    }
}
