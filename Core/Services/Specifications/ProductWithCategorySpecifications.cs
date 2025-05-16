using Domain;
using Domain.Contracts;
using Shared;

namespace Services.Specifications
{
    public class ProductWithCategorySpecifications : Specifications<Product>
    {
        public ProductWithCategorySpecifications(Guid id)
            :base(product=>product.productId==id)
        {
            //AddInclude(p => p.category);
        }
        public ProductWithCategorySpecifications(ProductSpecificationsParameters parameters)
            :base(product=>
            (!string.IsNullOrWhiteSpace(parameters.category) || product.category == parameters.category)
            &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.name.ToLower().Contains(parameters.Search.ToLower().Trim()))
            )
        {
            //AddInclude(p => p.category);
            if (parameters.Sort is not null)
            {
                // Sorting
                switch (parameters.Sort)
                {
                    case ProductSortOptions.PriceAsc:
                        SetOrderBy(p => p.price);
                        break;
                    case ProductSortOptions.PriceDesc:
                        SetOrderByDescending(p => p.price);
                        break;
                    case ProductSortOptions.NameDesc:
                        SetOrderByDescending(p => p.name);
                        break;
                    default:
                        SetOrderBy(p => p.name);
                        break;
                }
            }

            ApplyPagination(parameters.pageIndex, parameters.PageSize);
        }
    }
}
