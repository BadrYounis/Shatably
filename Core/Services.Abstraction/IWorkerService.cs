using Shared;

namespace Services.Abstraction;
public interface IWorkerService
{
    Task<IEnumerable<WorkerDto>> GetAllAsync();
    Task<WorkerDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<WorkerDto>> GetByTypeAsync(string type);
    Task<Guid> CreateAsync(CreateWorkerDto dto);
    Task UpdateAsync(Guid workerId, UpdateWorkerDto dto);
    Task DeleteAsync(Guid id);
}
