using Domain;
using Domain.Contracts;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class DesignTemplateCountSpecifications : Specifications<DesignTemplate>
    {
        public DesignTemplateCountSpecifications(DesignTemplateSpecificationsParameters parameters)
              : base(product =>
             (!string.IsNullOrWhiteSpace(parameters.category) || product.category == parameters.category)
             &&
             (string.IsNullOrWhiteSpace(parameters.Search) || product.name.ToLower().Contains(parameters.Search.ToLower().Trim()))
             )
        {
        }
    }
}
