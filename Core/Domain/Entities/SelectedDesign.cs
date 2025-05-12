namespace Domain
{
    public class SelectedDesign
    {
        public Guid SelectedDesignId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        // اختياري، يربط بـ DesignTemplate
        public Guid? DesignTemplateId { get; set; }
        public DesignTemplate? DesignTemplate { get; set; }

        // اختياري، يربط بـ ProjectPortfolio
        public Guid? ProjectPortfolioId { get; set; }
        public ProjectPortfolio? ProjectPortfolio { get; set; }
    }
}