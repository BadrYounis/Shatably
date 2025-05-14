using System.ComponentModel.DataAnnotations;

namespace Shared;
public class CreateProjectPortfolioDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Image URL is required.")]
    [Url(ErrorMessage = "Image URL must be a valid URL.")]
    public string mainImageUrl { get; set; }
    public HashSet<string>? ImageUrls { get; set; } 
}
