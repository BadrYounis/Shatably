namespace Domain;
public class RatingReview
{
    public Guid ReviewId { get; set; }        // Unique identifier for the review
    public int Rating { get; set; }           // Rating (e.g., from 1 to 5)
    public string? Comment { get; set; }       // Optional: client feedback
    public DateTime CreatedAt { get; set; }   // When the review was written
    
    // Navigation Properties
    public Client Client { get; set; } 
    public Guid ClientId { get; set; } 
    public Worker? Worker { get; set; } 
    public Guid? WorkerId { get; set; } 
    public ProjectPortfolio? ProjectPortfolio { get; set; }
    public Guid? ProjectPortfolioId { get; set; }

}

