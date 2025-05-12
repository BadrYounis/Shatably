using Shared;

namespace Services.Abstraction;
public interface IProjectPortfolioService
{
    Task<PaginatedResult<ProjectPortfolioDto>> GetAllAsync(ProjectSpecificationsParameters parameters);
    Task<ProjectPortfolioDto> GetByIdAsync(Guid projectId);
    Task<Guid> CreateAsync(CreateProjectPortfolioDto dto);
    Task UpdateAsync(Guid projectId, UpdateProjectPortfolioDto dto);
    Task DeleteAsync(Guid projectId);
}
