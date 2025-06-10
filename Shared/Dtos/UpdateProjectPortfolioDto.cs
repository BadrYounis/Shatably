using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared;
public class UpdateProjectPortfolioDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public HashSet<string>? ImageUrls { get; set; }
    public decimal Price { get; set; }
}