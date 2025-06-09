using Shared.Dtos;

namespace Shared;
public class CreateWorkerDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Type { get; set; } // enum string
    public string? ProfilePhotoUrl { get; set; }
    public int Experience { get; set; }
    public string ExperienceDisplay => $"{Experience} Year{(Experience == 1 ? "" : "s")}";
    public decimal Price { get; set; }
}