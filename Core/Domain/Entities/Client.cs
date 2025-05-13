namespace Domain;
public class Client
{
    public Guid ClientId { get; set; }
    public string fullName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public Address? Address { get; set; }
    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    public ICollection<RatingReview> RatingReviews { get; set; } = new HashSet<RatingReview>();
    public ICollection<ServiceBooking> ServiceBookings { get; set; } = new HashSet<ServiceBooking>();
}