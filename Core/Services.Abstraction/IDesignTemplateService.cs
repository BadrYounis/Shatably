using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IDesignTemplateService
    {
        Task<PaginatedResult<DesignTemplateDto>> GetAllAsync(DesignTemplateSpecificationsParameters parameters);
        Task<DesignTemplateDto> GetByIdAsync(Guid templateId);
        Task<IEnumerable<DesignTemplateDto>> GetByCategoryAsync(string category);
        Task<Guid> CreateAsync(CreateDesignTemplateDto dto);
        Task UpdateAsync(Guid templateId, UpdateDesignTemplateDto dto);
        Task DeleteAsync(Guid templateId);
    }
}
