using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);
        Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> GetByIdAsync(Specifications<TEntity> specifications);
        Task<int> CountAsync(Specifications<TEntity> specifications);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
