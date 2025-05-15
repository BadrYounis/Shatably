using Domain.Entities.OrderEntities;

namespace Domain;
public class Product
{
    public Guid productId { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public decimal price { get; set; }
    public int stockAmount { get; set; }
    public bool isPackage { get; set; }
}