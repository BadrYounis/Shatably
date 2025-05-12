namespace Shared;
public class ProjectPortfolioDto
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}