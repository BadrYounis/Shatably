using System;
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class UpdateUserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name can't exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name can't exceed 50 characters.")]
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public AddressDto? Address { get; set; }
    }
}
