namespace Shared
{
    public class BookingCreateDto
    {
        public DateTime BookingDate { get; set; }
        public string TimeSlot { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? WorkerId { get; set; }
        public Guid? PortfolioId { get; set; }
        public Guid? ServiceId { get; set; }
        public LocationDto Location { get; set; }
    }
}
