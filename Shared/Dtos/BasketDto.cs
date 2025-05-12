namespace Shared;
public class BasketDto
{
    public string Username { get; set; } = string.Empty;
    public IEnumerable<BasketItemDto> Items { get; set; } = new HashSet<BasketItemDto>();
}