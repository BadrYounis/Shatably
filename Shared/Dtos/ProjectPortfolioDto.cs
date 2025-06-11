using Shared.Dtos;

namespace Shared;
public class ProjectPortfolioDto
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string mainImageUrl { get; set; }
    public HashSet<string>? ImageUrls { get; set; }
    public double HourService { get; set; }
    public string HourServiceDisplay => $"{HourService} Hour Service";
    public decimal Price { get; set; }
    public ICollection<RatingReviewDto> RatingReview { get; set; } = new List<RatingReviewDto>();
}