using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
   public interface IClientService
    {
        Task<IEnumerable<ClientResultDto>> GetAllAsync();
        Task<ClientResultDto> GetByIdAsync(Guid clientId);
        Task<Guid> CreateAsync(CreateClientDto dto);
        Task UpdateAsync(Guid clientId, UpdateClientDto dto);
        Task DeleteAsync(Guid clientId);
    }
}
