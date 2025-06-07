using Microsoft.AspNetCore.Http;
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

    // نخليها nullable علشان هنحسبها من السيرفر بعد رفع الصورة
    [Url(ErrorMessage = "Main image URL must be a valid URL.")]
    // ممكن تكون null لو المشروع من النوع التاني اللي مفيهوش صور إضافية
    public string? mainImageUrl { get; set; }
    public HashSet<string>? ImageUrls { get; set; }

    //[Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue)]
    public decimal? Price { get; set; }
    //public IFormFile? MainImageFile { get; set; } 
    //public List<IFormFile>? GalleryImages { get; set; } 
}
