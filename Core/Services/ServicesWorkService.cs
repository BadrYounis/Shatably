using Domain;
using Domain.Contracts;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Services.Specifications;
using Shared.Dtos;

namespace Services
{
    public class ServicesWorkService : IServicesWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IGenericRepository<ServicesWork, Guid>> _ServicesRepository;
        public ServicesWorkService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _ServicesRepository = new Lazy<IGenericRepository<ServicesWork, Guid>>(
                () => _unitOfWork.GetRepository<ServicesWork, Guid>());
        }
        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = await _ServicesRepository.Value.GetAllAsync(new ServiceWithRatingReviewSpecifications());
            var result = services.Select(MapToDto);
            return result;
        }
        public async Task<ServiceDto> GetByIdAsync(Guid serviceId)
        {
            var service = await _ServicesRepository.Value.GetByIdAsync(new ServiceWithRatingReviewSpecifications(serviceId));
            return service is null ? throw new ServicesWorkNotFoundException(serviceId) : MapToDto(service);
        }
        public async Task<ServiceDto> CreateAsync(CreateServicesDto dto)
        {
            var service = new ServicesWork
            {
                ServiceId = Guid.NewGuid(),
                Name = dto.Name,
                Details = dto.Details,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl!,
                Price = dto.Price,
                AverageRating = dto.AverageRating,
                ReviewCount = dto.ReviewCount
            };
            await _ServicesRepository.Value.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceDto
            {
                ServiceId = service.ServiceId,
                Name = service.Name,
                Details = service.Details,
                Description = service.Description,
                ImageUrl = service.ImageUrl,
                Price = service.Price,
                AverageRating = dto.AverageRating,
                ReviewCount = dto.ReviewCount
            };
        }
        public async Task UpdateAsync(Guid serviceId, UpdateServiceDto dto)
        {
            var service = await _ServicesRepository.Value.GetByIdAsync(serviceId);
            if (service is null)
                throw new ServicesWorkNotFoundException(serviceId);

            service.Name = dto.Name;
            service.Details = dto.Details;
            service.Description = dto.Description;
            service.ImageUrl = dto.ImageUrl!;
            service.Price = dto.Price;

            _ServicesRepository.Value.Update(service);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid serviceId)
        {
            var service = await _ServicesRepository.Value.GetByIdAsync(serviceId);
            if (service is null)
                throw new ServicesWorkNotFoundException(serviceId);

            _ServicesRepository.Value.Delete(service);
            await _unitOfWork.SaveChangesAsync();
        }            
        private ServiceDto MapToDto(ServicesWork service) => new ServiceDto
        {
            ServiceId = service.ServiceId,
            Name = service.Name,
            Details = service.Details,
            Description = service.Description,
            ImageUrl = string.IsNullOrEmpty(service.ImageUrl) ? string.Empty : service.ImageUrl,
            Price = service.Price,
            AverageRating = service.AverageRating,
            ReviewCount = service.ReviewCount
            //AverageRating = service.RatingReviews.Any()
            //    ? service.RatingReviews.Average(r => r.Rating)
            //    : 0,
            //ReviewCount = service.RatingReviews.Count,           
        };
    }
}
