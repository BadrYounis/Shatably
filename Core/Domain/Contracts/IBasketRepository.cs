namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(string username);
        Task<Basket?> UpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string username);
    }
}
