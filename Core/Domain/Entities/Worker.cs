using Domain.Entities.OrderEntities;

namespace Domain;
public class Worker
{
    public Guid workerId { get; set; }
    public string fullName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public Type? Type { get; set; }
    public Acceptence? Acceptence { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public int Experience { get; set; }
    public decimal Price { get; set; }
    public Guid UserId { get; set; } // الربط مع Security DB
    public ICollection<ServiceBooking> ServiceBookings { get; set; } = new HashSet<ServiceBooking>();
    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    public ICollection<RatingReview> RatingReviews { get; set; } = new HashSet<RatingReview>();
}
public enum Type
{
    Electrician,
    Carpenter,
    Painter,
    Plumber,
    Maid
}
public enum Acceptence
{
    Accept,
    Decline
}
