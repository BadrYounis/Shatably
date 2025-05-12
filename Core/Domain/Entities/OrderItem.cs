namespace Domain;
public class OrderItem
{
    public Guid orderItemId { get; set; } 
    public int quantity { get; set; } 
    public decimal unitPrice { get; set; } 
    public Order Order { get; set; } 
    public Guid OrderId { get; set; } 
    public Product Product { get; set; } 
    public Guid ProductId { get; set; } 
}
