using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos
{
    public class UpdateServiceDto
    {
        [Required(ErrorMessage = "Service name is required.")]
        [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Details are required.")]
        [StringLength(300, ErrorMessage = "Details cannot exceed 300 characters.")]
        public string Details { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
    }
}
