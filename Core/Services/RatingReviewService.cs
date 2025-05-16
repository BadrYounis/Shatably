using Domain.Contracts;
using Domain;
using Services.Abstraction;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Services.Mapper;

namespace Services
{
    public class RatingReviewService : IRatingReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IGenericRepository<RatingReview, Guid>> _ratingRepository;
        private readonly Lazy<IGenericRepository<Client, Guid>> _clientRepository;
        private readonly Lazy<IGenericRepository<Worker, Guid>> _workerRepository;
        private readonly Lazy<IGenericRepository<ProjectPortfolio, Guid>> _projectRepository;
        public RatingReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ratingRepository = new Lazy<IGenericRepository<RatingReview, Guid>>(
                () => _unitOfWork.GetRepository<RatingReview, Guid>());
            _clientRepository = new Lazy<IGenericRepository<Client, Guid>>(
                () => _unitOfWork.GetRepository<Client, Guid>());
            _workerRepository = new Lazy<IGenericRepository<Worker, Guid>>(
                () => _unitOfWork.GetRepository<Worker, Guid>());
            _projectRepository = new Lazy<IGenericRepository<ProjectPortfolio, Guid>>(
                () => _unitOfWork.GetRepository<ProjectPortfolio, Guid>());
        }
        public async Task<IEnumerable<RatingReviewDto>> GetAllAsync()
        {
            var reviews = await _ratingRepository.Value.GetAllAsync();
            return reviews.Select(RatingReviewMapper.MapToRatingReviewDTO);
        }
        public async Task<RatingReviewDto> GetByIdAsync(Guid id)
        {
            var review = await _ratingRepository.Value.GetByIdAsync(id);

            if (review == null)
                throw new NotFoundException("Review not found.");

            return RatingReviewMapper.MapToRatingReviewDTO(review);
        }
        public async Task<RatingReviewDto> AddAsync(CreateRatingReviewDto dto)
        {
            if (dto.WorkerId == null && dto.ProjectPortfolioId == null)
                throw new ArgumentException("You must provide either WorkerId or ProjectPortfolioId.");

            if (dto.WorkerId != null && dto.ProjectPortfolioId != null)
                throw new ArgumentException("You can only rate either a worker or a project, not both.");

            var client = await _clientRepository.Value.GetByIdAsync(dto.ClientId);
            if (client == null)
                throw new NotFoundException("Client not found.");

            if (dto.WorkerId.HasValue)
            {
                var worker = await _workerRepository.Value.GetByIdAsync(dto.WorkerId.Value);
                if (worker == null)
                    throw new NotFoundException("Worker not found.");
            }

            if (dto.ProjectPortfolioId.HasValue)
            {
                var project = await _projectRepository.Value.GetByIdAsync(dto.ProjectPortfolioId.Value);
                if (project == null)
                    throw new NotFoundException("Project not found.");
            }

            var review = RatingReviewMapper.MapToRatingReview(dto);

            await _ratingRepository.Value.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return RatingReviewMapper.MapToRatingReviewDTO(review);
        }
        public async Task UpdateAsync(Guid id, UpdateRatingReviewDto dto)
        {
            var review = await _ratingRepository.Value.GetByIdAsync(id);
            if (review == null)
                throw new NotFoundException("Review Not Found!");

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            _ratingRepository.Value.Update(review);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var review = await _ratingRepository.Value.GetByIdAsync(id);
            if (review is null)
                throw new NotFoundException("Review Not Found!");

            _ratingRepository.Value.Delete(review);
            await _unitOfWork.SaveChangesAsync();
        }       
    }
}
