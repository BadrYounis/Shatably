namespace Domain;
public class ProjectPortfolio
{
    public Guid projectId { get; set; }
    public string Title { get; set; }
    public string description { get; set; }
    public string imageUrl { get; set; }
    public DateTime createdDate { get; set; }
}