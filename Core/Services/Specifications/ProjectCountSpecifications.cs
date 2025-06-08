using Domain;
using Domain.Contracts;
using Shared;

namespace Services.Specifications
{
    public class ProjectCountSpecifications : Specifications<ProjectPortfolio>
    {
        public ProjectCountSpecifications(ProjectSpecificationsParameters parameters)
          : base(project =>
          ((string.IsNullOrWhiteSpace(parameters.Title)) || project.Title == parameters.Title)
          &&
          (string.IsNullOrWhiteSpace(parameters.Search) || project.Title.ToLower().Contains(parameters.Search.ToLower().Trim()))
          )
        {
        }
    }
}
