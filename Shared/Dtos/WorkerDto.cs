using Shared.Dtos;

namespace Shared;
public class WorkerDto
{
    public Guid WorkerId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Type { get; set; } //map enum to string
    public string? ProfilePhotoUrl { get; set; }
    public int Experience { get; set; }
    public string ExperienceDisplay => $"{Experience} Year{(Experience == 1 ? "" : "s")}";
    public decimal Price { get; set; }
    public ICollection<RatingReviewDto> RatingReview { get; set; }=new List<RatingReviewDto>();
   
}

