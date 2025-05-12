using Domain;
using Domain.Contracts;
using Domain.Exceptions;
using Services.Abstraction;
using Services.Specifications;
using Shared;

namespace Services
{
    public class ProjectPortfolioService : IProjectPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IGenericRepository<ProjectPortfolio, Guid>> _projectRepository;
        public ProjectPortfolioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = new Lazy<IGenericRepository<ProjectPortfolio, Guid>>(() => _unitOfWork.GetRepository<ProjectPortfolio, Guid>());
        }
        public async Task<PaginatedResult<ProjectPortfolioDto>> GetAllAsync(ProjectSpecificationsParameters parameters)
        {
          var projects = await _projectRepository.Value.GetAllAsync(new ProjectSpecifications(parameters));     //guid???
           // var projects=await _unitOfWork.GetRepository<ProjectPortfolio,Guid>().GetAllAsync(new ProjectSpecifications(parameters));
            var totalCount = await _unitOfWork.GetRepository<ProjectPortfolio, Guid>().CountAsync(new ProjectCountSpecifications(parameters));
            var projectResult = projects.Select(MapToDto);
            var result = new PaginatedResult<ProjectPortfolioDto>(
                projectResult.Count(),
                parameters.pageIndex,
                totalCount,
                projectResult
                );
            return result;
        }
        public async Task<ProjectPortfolioDto> GetByIdAsync(Guid projectId)
        {
          var project = await _projectRepository.Value.GetByIdAsync(new ProjectSpecifications(projectId));
           // var project = await _unitOfWork.GetRepository<ProjectPortfolio,Guid>().GetByIdAsync(new ProjectSpecifications(projectId));
            if (project is null)
                throw new NotFoundException("Project Portfolio Not Found.");
            return MapToDto(project);
        }
        public async Task<Guid> CreateAsync(CreateProjectPortfolioDto dto)
        {
            var project = new ProjectPortfolio
            {
                projectId = Guid.NewGuid(),
                Title = dto.Title,
                description = dto.Description,
                imageUrl = dto.ImageUrl,
                createdDate = DateTime.UtcNow
            };

            await _projectRepository.Value.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();
            return project.projectId;
        }
        public async Task UpdateAsync(Guid projectId, UpdateProjectPortfolioDto dto)
        {
            var project = await _projectRepository.Value.GetByIdAsync(projectId);
            if (project is null)
                throw new NotFoundException("Project Portfolio Not Found.");
            project.Title = dto.Title;
            project.description = dto.Description;
            project.imageUrl = dto.ImageUrl;
            _projectRepository.Value.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid projectId)
        {
            var project = await _projectRepository.Value.GetByIdAsync(projectId);
            if (project is null)
                throw new NotFoundException("Project Portfolio Not Found.");
            _projectRepository.Value.Delete(project);
            await _unitOfWork.SaveChangesAsync();
        }
        private static ProjectPortfolioDto MapToDto(ProjectPortfolio project) => new ProjectPortfolioDto
        {
            ProjectId = project.projectId,
            Title = project.Title,
            Description = project.description,
            ImageUrl = project.imageUrl,
            CreatedDate = project.createdDate
        };
    }
}
