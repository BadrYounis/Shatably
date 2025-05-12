namespace Shared;
public class UserLoginDto
{
    public string? Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string? Username { get; set; }
}