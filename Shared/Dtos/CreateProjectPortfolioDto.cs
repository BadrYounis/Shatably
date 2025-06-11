using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared;
public class CreateProjectPortfolioDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? mainImageUrl { get; set; }
    public HashSet<string>? ImageUrls { get; set; }
    public double HourService { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal? Price { get; set; }
}
