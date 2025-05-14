using Domain;
using Domain.Contracts;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Services.Specifications;
using Shared;

namespace Services
{

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IGenericRepository<Product, Guid>> _productRepository;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = new Lazy<IGenericRepository<Product, Guid>>(
                () => _unitOfWork.GetRepository<Product, Guid>());
        }
        public async Task<PaginatedResult<ProductDto>> GetAllAsync(ProductSpecificationsParameters parameters)
        {
              var products = await _productRepository.Value.GetAllAsync(new ProductWithCategorySpecifications(parameters));
           // var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithCategorySpecifications(parameters));
            var totalCount = await _unitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecifications(parameters));
            var productResult = products.Select(MapToDto);
            var result = new PaginatedResult<ProductDto>(
                productResult.Count(),
                parameters.pageIndex,
                totalCount,
                productResult
                );
            return result;
        }
        public async Task<ProductDto> GetByIdAsync(Guid productId)
        {
            var product = await _productRepository.Value.GetByIdAsync(new ProductWithCategorySpecifications(productId));
            // var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithCategorySpecifications(productId));
            //if (product is null)
            //    throw new NotFoundException("Product not found.");
            //return MapToDto(product);
            return product is null ? throw new ProductNotFoundException(productId) : MapToDto(product);

        }
        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category)
        {
            var products = await _productRepository.Value.FindAsync(p => p.category == category);
            return products.Select(MapToDto);
        }
        public async Task<Guid> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                productId = Guid.NewGuid(),
                name = dto.Name,
                category = dto.Category,
                price = dto.Price,
                stockAmount = dto.StockAmount
            };
            await _productRepository.Value.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product.productId;
        }
        public async Task UpdateAsync(Guid productId, UpdateProductDto dto)
        {
            var product = await _productRepository.Value.GetByIdAsync(productId);
            if (product is null)
                throw new ProductNotFoundException(productId);
            product.name = dto.Name;
            product.category = dto.Category;
            product.price = dto.Price;
            product.stockAmount = dto.StockAmount;
            _productRepository.Value.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid productId)
        {
            var product = await _productRepository.Value.GetByIdAsync(productId);
            if (product is null)
                throw new ProductNotFoundException(productId);

            _productRepository.Value.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
        private static ProductDto MapToDto(Product product) => new ProductDto
        {
            ProductId = product.productId,
            Name = product.name,
            Category = product.category,
            Price = product.price,
            StockAmount = product.stockAmount,
            
        };

    }
}
