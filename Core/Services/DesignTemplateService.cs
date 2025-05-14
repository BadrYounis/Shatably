using Domain.Contracts;
using Domain;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;
using Services.Specifications;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class DesignTemplateService : IDesignTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IGenericRepository<DesignTemplate, Guid>> _designTemplateRepository;
        private readonly string? _imageBaseUrl;
        public DesignTemplateService(IUnitOfWork unitOfWork , IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _designTemplateRepository = new Lazy<IGenericRepository<DesignTemplate, Guid>>(
                () => _unitOfWork.GetRepository<DesignTemplate, Guid>());
            _imageBaseUrl = configuration["BaseUrls:ImageBaseUrl"];
        }
        public async Task<PaginatedResult<DesignTemplateDto>> GetAllAsync(DesignTemplateSpecificationsParameters parameters)
        {
            var designs = await _designTemplateRepository.Value.GetAllAsync(new DesignTemplateWithCategorySpecifications(parameters));
            var totalCount = await _unitOfWork.GetRepository<DesignTemplate, Guid>().CountAsync(new DesignTemplateCountSpecifications(parameters));
            var designResult = designs.Select(MapToDto);
            var result = new PaginatedResult<DesignTemplateDto>(
                designResult.Count(),
                parameters.pageIndex,
                totalCount,
                designResult
                );
            return result;

        }

        public async Task<DesignTemplateDto> GetByIdAsync(Guid templateId)
        {
            var design = await _designTemplateRepository.Value.GetByIdAsync(new DesignTemplateWithCategorySpecifications(templateId));
           

            return design is null ? throw new DesignTemplateNotFoundException(templateId) : MapToDto(design);
        }
        public async Task<IEnumerable<DesignTemplateDto>> GetByCategoryAsync(string category)
        {
            var designs = await _designTemplateRepository.Value.FindAsync(d => d.category == category);
            return designs.Select(MapToDto);
        }
        public async Task<Guid> CreateAsync(CreateDesignTemplateDto dto)
        {
            var design = new DesignTemplate
            {
                templateId = Guid.NewGuid(),
                name = dto.Name,
                category = dto.Category,
                description = dto.Description,
                imageUrl=dto.imageUrl,
                
            };
            await _designTemplateRepository.Value.AddAsync(design);
            await _unitOfWork.SaveChangesAsync();
            return design.templateId;
        }

        public async Task UpdateAsync(Guid templateId, UpdateDesignTemplateDto dto)
        {
            var design = await _designTemplateRepository.Value.GetByIdAsync(templateId);
            if (design is null)
                throw new DesignTemplateNotFoundException(templateId);
            design.name = dto.Name;
            design.category = dto.Category;
            _designTemplateRepository.Value.Update(design);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid templateId)
        {
            var design = await _designTemplateRepository.Value.GetByIdAsync(templateId);
            if (design is null)
                throw new DesignTemplateNotFoundException(templateId);

            _designTemplateRepository.Value.Delete(design);
            await _unitOfWork.SaveChangesAsync();
        }

        
        private DesignTemplateDto MapToDto(DesignTemplate design) => new DesignTemplateDto
        {
             templateId= design.templateId,
             Name= design.name,
             Category= design.category,
             description= design.description,
            imageUrl = string.IsNullOrEmpty(design.imageUrl)
                ? null
                : $"{_imageBaseUrl}/{design.imageUrl}"

        };
    }
}
