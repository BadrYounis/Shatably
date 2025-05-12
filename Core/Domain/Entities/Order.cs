namespace Domain;
public class Order
{
    public Guid OrderId { get; set; } 
    public DateTime OrderDate { get; set; } 
    public decimal TotalPrice { get; set; } 
    public string Status {  get; set; } 
    public Client Client { get; set; } 
    public Guid ClientId { get; set; }
    public Worker Worker { get; set; }
    public Guid WorkerId { get; set; }
    //One-to-Many With OrderItems
    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    //One-to-Many With SelectedDesign
    public ICollection<SelectedDesign> SelectedDesigns { get; set; } = new HashSet<SelectedDesign>();
}