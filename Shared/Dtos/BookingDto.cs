namespace Shared
{
    public class BookingDto
    {
        public Guid BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public string TimeSlot { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? WorkerId { get; set; }
        public Guid? PortfolioId { get; set; }
        public Guid? ServiceId { get; set; }
        public LocationDto Location { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
    }
}
