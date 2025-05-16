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
    public class ProjectSpecifications:Specifications<ProjectPortfolio>
    {
        public ProjectSpecifications(Guid Id):base(project=>project.projectId==Id)
        {
            AddInclude(p => p.Images); // Include الصور
        }
        public ProjectSpecifications(ProjectSpecificationsParameters parameters)
           : base(project =>
           (!(string.IsNullOrWhiteSpace(parameters.Title)) || project.Title == parameters.Title)
           &&
           (string.IsNullOrWhiteSpace(parameters.Search) || project.Title.ToLower().Contains(parameters.Search.ToLower().Trim()))
           )
        {
            //AddInclude(p => p.Title);

            if (parameters.Sort is not null)
            {
                // Sorting
                switch (parameters.Sort)
                {
                    case ProjectSortOptions.IdAsc:
                        SetOrderBy(p => p.projectId);
                        break;
                    case ProjectSortOptions.IdDesc:
                        SetOrderByDescending(p => p.projectId);
                        break;
                    case ProjectSortOptions.TitleDesc:
                        SetOrderByDescending(p => p.Title);
                        break;
                    default:
                        SetOrderBy(p => p.Title);
                        break;
                }
            }

            ApplyPagination(parameters.pageIndex, parameters.PageSize);
        }
    }
}
