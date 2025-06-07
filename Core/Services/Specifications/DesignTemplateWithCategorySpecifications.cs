using Domain;
using Domain.Contracts;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class DesignTemplateWithCategorySpecifications:Specifications<DesignTemplate>
    {
        public DesignTemplateWithCategorySpecifications(Guid id)
          : base(product => product.templateId == id)
        {
        }
        public DesignTemplateWithCategorySpecifications(DesignTemplateSpecificationsParameters parameters)
            : base(product =>
            (!string.IsNullOrWhiteSpace(parameters.category) || product.category == parameters.category)
            &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.name.ToLower().Contains(parameters.Search.ToLower().Trim()))
            )
        {
            AddInclude(p => p.category);
            if (parameters.Sort is not null)
            {
                // Sorting
                switch (parameters.Sort)
                {
                    //case ProductSortOptions.PriceAsc:
                    //    SetOrderBy(p => p.price);
                    //    break;
                    //case ProductSortOptions.PriceDesc:
                    //    SetOrderByDescending(p => p.price);
                    //    break;
                    case DesignTemplateSortOptions.NameDesc:
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
