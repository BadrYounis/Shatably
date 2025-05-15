using Domain;
using Domain.Contracts;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Shared;
using Shared.Dtos;

namespace Services;
public class WorkerService : IWorkerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Lazy<IGenericRepository<Worker, Guid>> _workerRepository;
    private readonly string? _imageBaseUrl;
    public WorkerService(IUnitOfWork unitOfWork,IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _workerRepository = new Lazy<IGenericRepository<Worker, Guid>>(
                () => _unitOfWork.GetRepository<Worker, Guid>());
        _imageBaseUrl = configuration["BaseUrls:ImageBaseUrl"];

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
            ProfilePhotoUrl = dto.ProfilePhotoUrl,
            Price = dto.Price,
            Experience= dto.Experience,
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
        worker.Price = dto.Price;
        worker.Experience = dto.Experience;
    
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
    private  WorkerDto ToDto(Worker worker)
    {
        return new WorkerDto
        {
            WorkerId = worker.workerId,
            FullName = worker.fullName,
            Email = worker.email,
            PhoneNumber = worker.phoneNumber,
            Type = worker.Type.ToString()!,
            ProfilePhotoUrl = string.IsNullOrEmpty(worker.ProfilePhotoUrl)
                ? string.Empty
                : $"{_imageBaseUrl}/{worker.ProfilePhotoUrl}",
            Price = worker.Price,
            Experience = worker.Experience,
            RatingReview = (worker.RatingReviews != null && worker.RatingReviews.Any())
                         ? worker.RatingReviews.Select(r => new RatingReviewDto
                         {
                             Rating = r.Rating,
                             Comment = r.Comment ?? string.Empty,
                             CreatedAt = r.CreatedAt
                         }).ToList()
                         : new List<RatingReviewDto>()
        };
    }
}
