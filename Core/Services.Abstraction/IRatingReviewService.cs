using Shared.Dtos;

namespace Services.Abstraction
{
    public interface IRatingReviewService
    {
        Task<IEnumerable<RatingReviewDto>> GetAllAsync();
        Task<RatingReviewDto> GetByIdAsync(Guid id);
        Task<RatingReviewDto> AddAsync(CreateRatingReviewDto review);
        Task UpdateAsync(Guid id, UpdateRatingReviewDto review);
        Task DeleteAsync(Guid id);
    }
}
