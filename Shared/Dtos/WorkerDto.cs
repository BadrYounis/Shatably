namespace Shared;
public class WorkerDto
{
    public Guid WorkerId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Type { get; set; } //map enum to string
    public string? ProfilePhotoUrl { get; set; }
}

