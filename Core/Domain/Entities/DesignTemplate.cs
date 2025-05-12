namespace Domain;
public class DesignTemplate
{
    public Guid templateId { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public string imageUrl { get; set; }
    public string description { get; set; }
    public ICollection<SelectedDesign> SelectedDesigns { get; set; } = new HashSet<SelectedDesign>();
}