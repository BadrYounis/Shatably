using System.ComponentModel.DataAnnotations;

namespace Shared;
public class BasketItemDto
{
    //public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }
    [Range(1, 99)]
    public int Quantity { get; set; }
}

