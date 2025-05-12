namespace Shared;
public class RemoveBasketItemDto
{
    public string Username { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
}