using System.ComponentModel.DataAnnotations;

namespace Shared;
public record RegisterDto
{
    public string? UserName { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; init; }

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; }
    public string? PhoneNumber { get; init; }
}
