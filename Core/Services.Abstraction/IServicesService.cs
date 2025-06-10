using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServicesWorkService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto> GetByIdAsync(Guid serviceId);
        Task<ServiceDto> CreateAsync(CreateServicesDto dto);
        Task UpdateAsync(Guid serviceId, UpdateServiceDto dto);
        Task DeleteAsync(Guid serviceId);
    }
}
