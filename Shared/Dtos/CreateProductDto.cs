using System.ComponentModel.DataAnnotations;

namespace Shared;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int StockAmount { get; set; }
    public string? ImageUrl { get; set; }
    public bool isPackage { get; set; }

}