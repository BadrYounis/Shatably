namespace Domain;
public class ServicesWork
{
    public Guid ServiceId { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public ICollection<RatingReview> RatingReviews { get; set; } = new HashSet<RatingReview>();
    public ICollection<ServiceBooking> ServiceBookings { get; set; } = new HashSet<ServiceBooking>();
}