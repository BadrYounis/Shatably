namespace Domain;
public class RatingReview
{
    public Guid ReviewId { get; set; }        // Unique identifier for the review
    public int Rating { get; set; }           // Rating (e.g., from 1 to 5)
    public string? Comment { get; set; }       // Optional: client feedback
    public DateTime CreatedAt { get; set; }   // When the review was written

    // Navigation Properties
    public Worker Worker { get; set; } // The worker being reviewed
    public Guid WorkerId { get; set; }        // Foreign Key to WorkerProfile
    public Client Client { get; set; } // The client who wrote the review
    public Guid ClientId { get; set; }        // Foreign Key to ClientProfile

}

