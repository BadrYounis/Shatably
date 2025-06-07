namespace Services.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IProjectPortfolioService ProjectPortfolioService { get; }
        public IAuthService AuthService { get; }
        public IDesignTemplateService DesignTemplateService { get; }
        public IClientService ClientService { get; }
        public IWorkerService WorkerService { get; }
        public IOrderService OrderService { get; }
        public IRatingReviewService RatingReviewService { get; }
        public IPaymentService PaymentService { get; }
        public IImageService ImageService { get; }

    }
}
