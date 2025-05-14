namespace Domain;
public class ServiceBooking
{
    public Guid bookingId { get; set; }
    public DateTime bookingDate { get; set; }
    public string Status { get; set; }
    public string TimeSlot { get; set; }
    public Client Client { get; set; }  //Navigation to Client
    public Guid ClientId { get; set; }
    public Worker Worker { get; set; }  //Navigation to Worker
    public Guid WorkerId { get; set; }
}