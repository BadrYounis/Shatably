namespace Shared;
public class AddUserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // For Identity
    public string FirstName { get; set; }
    public string LastName { get; set; }
}