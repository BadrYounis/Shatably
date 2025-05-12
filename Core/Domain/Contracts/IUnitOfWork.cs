namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        // SaveChanges() => Complete
        Task<int> SaveChangesAsync();
        // Signture for Function will return an instance of class that implement IGenericRepo
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;
    }
}
