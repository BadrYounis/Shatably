namespace Shared.Dtos
{
    public class RatingReviewDto
    {
        public Guid ReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ClientId { get; set; }
    }
}
