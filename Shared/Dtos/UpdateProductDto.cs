using System.ComponentModel.DataAnnotations;

namespace Shared;
public class UpdateProductDto
{
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters.")]
    public string Category { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Stock amount is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock amount cannot be negative.")]
    public int StockAmount { get; set; }
}
