namespace Domain;
public class ProjectImages
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }

    // Foreign key
    public Guid ProjectId { get; set; }
    public ProjectPortfolio ProjectPortfolio { get; set; }
}
