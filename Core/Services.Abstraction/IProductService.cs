using Shared;

namespace Services.Abstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetAllAsync(ProductSpecificationsParameters parameters);
        Task<ProductDto> GetByIdAsync(Guid productId);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category);
        Task<Guid> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(Guid productId, UpdateProductDto dto);
        Task DeleteAsync(Guid productId);
    }
}
