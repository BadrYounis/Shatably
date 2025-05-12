using Shared;

namespace Services.Abstraction
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string username);
        Task<BasketDto?> UpdateBasketAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string username);
    }
}
