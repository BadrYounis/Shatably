using Domain;
using Domain.Contracts;
using Domain.Exceptions;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository, IProductService _productService) : IBasketService
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
            var basket = await MapToEntityAsync(basketDto);
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
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity,
                ImageUrl = item.ImageUrl,

            }).ToList()
        };

        // Manual Mapping: BasketDto -> Basket
        private async Task<Basket> MapToEntityAsync(BasketDto dto)
        {
            var items = new List<BasketItem>();

            foreach (var item in dto.Items)
            {

                var product = await _productService.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new NotFoundException($"Product with ID {item.ProductId} not found");

                items.Add(new BasketItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    ImageUrl = product.imageUrl
                });
            }

            return new Basket
            {
                Username = dto.Username,
                Items = items
            };
        }

    }
}
