using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class UpdateClientDto
    {
        [Required(ErrorMessage = "Client name is required.")]
        [StringLength(100, ErrorMessage = "client name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Client Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public AddressDto Address { get; set; }


    }
}
