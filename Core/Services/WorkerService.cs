using Domain;
using Domain.Contracts;
using Domain.Exceptions;
using Services.Abstraction;
using Shared;

namespace Services;
public class WorkerService : IWorkerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Lazy<IGenericRepository<Worker, Guid>> _workerRepository;
    public WorkerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _workerRepository = new Lazy<IGenericRepository<Worker, Guid>>(
                () => _unitOfWork.GetRepository<Worker, Guid>());
    }
    public async Task<IEnumerable<WorkerDto>> GetAllAsync()
    {
        var workers = await _workerRepository.Value.GetAllAsync();
        return workers.Select(ToDto);
    }
    public async Task<WorkerDto?> GetByIdAsync(Guid id)
    {
        var worker = await _workerRepository.Value.GetByIdAsync(id);
        if (worker is null)
            throw new WorkerNotFoundException(id);

        return ToDto(worker);
    }
    public async Task<IEnumerable<WorkerDto>> GetByTypeAsync(string type)
    {
        if (!Enum.TryParse<Domain.Type>(type, true, out var workerType))
            throw new ArgumentException($"Invalid worker type: {type}");

        var workers = await _workerRepository.Value.GetAllAsync();
        return workers.Where(w => w.Type == workerType).Select(ToDto);
    }
    public async Task<Guid> CreateAsync(CreateWorkerDto dto)
    {
        if (!Enum.TryParse<Domain.Type>(dto.Type, true, out var parsedType))
            throw new ArgumentException($"Invalid type value: {dto.Type}");

        var worker = new Worker
        {
            workerId = Guid.NewGuid(),
            fullName = dto.FullName,
            email = dto.Email,
            phoneNumber = dto.PhoneNumber,
            Type = parsedType,
            ProfilePhotoUrl = dto.ProfilePhotoUrl
        };

        await _workerRepository.Value.AddAsync(worker);
        await _unitOfWork.SaveChangesAsync();

        return worker.workerId;
    }
    public async Task UpdateAsync(Guid workerId, UpdateWorkerDto dto)
    {
        var worker = await _workerRepository.Value.GetByIdAsync(workerId);
    
        if (worker is null)
            throw new WorkerNotFoundException(workerId);
    
        if (!Enum.TryParse<Domain.Type>(dto.Type, true, out var parsedType))
            throw new ArgumentException($"Invalid type value: {dto.Type}");
    
        worker.fullName = dto.FullName;
        worker.email = dto.Email;
        worker.phoneNumber = dto.PhoneNumber;
        worker.Type = parsedType;
        worker.ProfilePhotoUrl = dto.ProfilePhotoUrl;
    
        _workerRepository.Value.Update(worker);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var worker = await _workerRepository.Value.GetByIdAsync(id);
        if (worker is null)
            throw new WorkerNotFoundException(id);

        _workerRepository.Value.Delete(worker);
        await _unitOfWork.SaveChangesAsync();
    }
    private static WorkerDto ToDto(Worker worker)
    {
        return new WorkerDto
        {
            WorkerId = worker.workerId,
            FullName = worker.fullName,
            Email = worker.email,
            PhoneNumber = worker.phoneNumber,
            Type = worker.Type.ToString()!,
            ProfilePhotoUrl = worker.ProfilePhotoUrl
        };
    }
}
