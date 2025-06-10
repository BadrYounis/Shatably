namespace Shared
{
    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Address { get; set; } // eg: "Alexandria, Smouha"
        public string? Description { get; set; } // eg: "Near XYZ café"
    }

}
