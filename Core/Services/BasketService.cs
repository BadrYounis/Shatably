using Domain;
using Domain.Contracts;
using Domain.Exceptions;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository) : IBasketService
    {
        public async Task<BasketDto?> GetBasketAsync(string username)
        {
            var basket = await _basketRepository.GetBasketAsync(username);
            return basket is null
                ? throw new NotFoundException("Basket not found.")
                :MapToDto(basket);
        }
        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto)
        {
            var basket = MapToEntity(basketDto);
            var updated = await _basketRepository.UpdateBasketAsync(basket);
            return updated is null
                ? throw new InvalidOperationException("Cannot update the basket now.")
                : MapToDto(updated);
        }
        public async Task<bool> DeleteBasketAsync(string username)
            => await _basketRepository.DeleteBasketAsync(username); 
        
        // Manual Mapping: Basket -> BasketDto
        private static BasketDto MapToDto(Basket basket) => new BasketDto
        {
            Username = basket.Username,
            Items = basket.Items.Select(item => new BasketItemDto
            {
                //ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };

        // Manual Mapping: BasketDto -> Basket
        private static Basket MapToEntity(BasketDto dto) => new Basket
        {
            Username = dto.Username,
            Items = dto.Items.Select(item => new BasketItem
            {
                //ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };
    }
}
