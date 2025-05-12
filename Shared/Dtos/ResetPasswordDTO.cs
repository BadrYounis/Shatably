using System.ComponentModel.DataAnnotations;

namespace Shared;
public class ResetPasswordDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    public string NewPassword { get; set; } = string.Empty;
    [Required]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}