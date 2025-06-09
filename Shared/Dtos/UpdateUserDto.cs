using System;
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsActive { get; set; }
        public AddressDto? Address { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
