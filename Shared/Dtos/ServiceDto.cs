namespace Shared.Dtos
{
    public class ServiceDto
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
