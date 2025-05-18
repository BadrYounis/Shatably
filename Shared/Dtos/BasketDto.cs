namespace Shared;
public class BasketDto
{
    public string Username { get; set; } = string.Empty;
    public IEnumerable<BasketItemDto> Items { get; set; } = new HashSet<BasketItemDto>();

    // Payment Module
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public decimal? ShippingPrice { get; set; }
    public int? DeliveryMethodId { get; set; }
}