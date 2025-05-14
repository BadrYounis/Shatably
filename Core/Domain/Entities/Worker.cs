using Domain.Entities;

namespace Domain;
public class Worker
{
    public Guid workerId { get; set; }
    public string fullName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public Type? Type { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public ICollection<ServiceBooking> ServiceBookings { get; set; } = new HashSet<ServiceBooking>();
    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    public ICollection<RatingReview> RatingReviews { get; set; } = new HashSet<RatingReview>();

    /// Placeholder for future Schedule relationship
    /// public ICollection<Schedule> Schedules { get; set; } 
}
public enum Type
{
    Electrician,
    Carpenter,
    Painter,
    Plumber,
    Maid
}
