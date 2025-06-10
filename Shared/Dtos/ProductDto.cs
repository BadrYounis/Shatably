namespace Shared;
public class ProductDto
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public string Details { get; set; }
    public int? StockAmount { get; set; }
    public string? imageUrl { get; set; }
    public bool isPackage { get; set; }
}