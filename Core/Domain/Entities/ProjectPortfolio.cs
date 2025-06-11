namespace Domain;
public class ProjectPortfolio
{
    public Guid projectId { get; set; }
    public string Title { get; set; }
    public string description { get; set; }
    public string MainImageUrl { get; set; }
    public DateTime createdDate { get; set; }
    public decimal Price { get; set; }
    public double HourService { get; set; }
    public string HourServiceDisplay => $"{HourService} Hour Service";
    public ICollection<ProjectImages> Images { get; set; } = new HashSet<ProjectImages>();
    public ICollection<RatingReview> RatingReviews { get; set; } = new HashSet<RatingReview>();
    public ICollection<ServiceBooking> ServiceBookings { get; set; } = new HashSet<ServiceBooking>();
}