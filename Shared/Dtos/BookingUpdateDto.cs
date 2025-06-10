namespace Shared
{
    public class BookingUpdateDto
    {
        public Guid BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string TimeSlot { get; set; }
        public string Status { get; set; }
        public Guid PortfolioId { get; set; }
        public Guid ServiceId { get; set; }
        public LocationDto Location { get; set; }
    }

}
