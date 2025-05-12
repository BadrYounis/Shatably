namespace Services.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IProjectPortfolioService ProjectPortfolioService { get; }
        public IAuthService AuthService { get; }
    }
}
