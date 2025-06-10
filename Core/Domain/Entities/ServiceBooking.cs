using Domain;
public class ServiceBooking
{
    public Guid BookingId { get; set; }
    public DateTime BookingDate { get; set; }
    public string Status { get; set; }
    public string TimeSlot { get; set; }
    public Client? Client { get; set; }
    public Guid? ClientId { get; set; }
    public Worker? Worker { get; set; }
    public Guid? WorkerId { get; set; }
    public ProjectPortfolio? Portfolio { get; set; }
    public Guid? PortfolioId { get; set; }
    public ServicesWork? Services { get; set; }
    public Guid? ServiceId { get; set; }

    // Payment
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingPaymentStatus PaymentStatus { get; set; } = BookingPaymentStatus.Pending;

    // Location
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; } // eg: "Alexandria, Smouha"
    public string Description { get; set; } // eg: "Near XYZ café"
}
public enum BookingPaymentStatus
{
    Pending,
    PaymentReceived,
    PaymentFailed
}
