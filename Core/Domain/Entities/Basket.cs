namespace Domain;
public class Basket
{
    public string Username { get; set; } // Acts as the Redis key
    public IEnumerable<BasketItem> Items { get; set; } = new HashSet<BasketItem>();
}
public class BasketItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
