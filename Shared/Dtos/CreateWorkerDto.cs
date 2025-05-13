namespace Shared;
public class CreateWorkerDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Type { get; set; } // enum string
    public string? ProfilePhotoUrl { get; set; }
}