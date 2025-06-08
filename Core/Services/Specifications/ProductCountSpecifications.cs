using Domain;
using Domain.Contracts;
using Shared;

namespace Services.Specifications
{
    public class ProductCountSpecifications : Specifications<Product>
    {
        public ProductCountSpecifications(ProductSpecificationsParameters parameters)
             : base(product =>
            (string.IsNullOrWhiteSpace(parameters.category) || product.category == parameters.category)
            &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.name.ToLower().Contains(parameters.Search.ToLower().Trim()))
            )
        {
        }
    }
}
