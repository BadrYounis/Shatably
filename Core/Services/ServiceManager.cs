using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Persistence.Data;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IProjectPortfolioService> _projectPortfolioService;
        private readonly Lazy<IAuthService> _AuthService;
        private readonly Lazy<IServicesWorkService> _ServicesWorkService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IClientService> _clientService;
        private readonly Lazy<IWorkerService> _workerService;
        private readonly Lazy<IRatingReviewService> _ratingReviewService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<IImageService> _imageService;
        public ServiceManager(IUnitOfWork unitOfWork, IBasketRepository _basketRepository, UserManager<User> _userManager,
            IOptions<JwtOptions> _options, AppDbContext _context, IEmailService _emailService
            , IHttpContextAccessor _httpContextAccessor, LinkGenerator _linkGenerator, IConfiguration configuration, IWebHostEnvironment _env, IProductService _productsService)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork));

            _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _productsService));

            _projectPortfolioService = new Lazy<IProjectPortfolioService>(() => new ProjectPortfolioService(unitOfWork, configuration));

            _AuthService = new Lazy<IAuthService>(() => new AuthService(_userManager, _options, _emailService, _httpContextAccessor, unitOfWork, _env));

            _ServicesWorkService = new Lazy<IServicesWorkService>(() => new ServicesWorkService(unitOfWork, configuration));

            _clientService = new Lazy<IClientService>(() => new ClientService(unitOfWork));

            _workerService = new Lazy<IWorkerService>(() => new WorkerService(unitOfWork, configuration));

            _orderService = new Lazy<IOrderService>(() => new OrderService(unitOfWork, _basketRepository));

            _ratingReviewService = new Lazy<IRatingReviewService>(() => new RatingReviewService(unitOfWork));

            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, configuration, _basketRepository));

            _imageService = new Lazy<IImageService>(() => new ImageService(_env));

        }
        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;
        public IProjectPortfolioService ProjectPortfolioService => _projectPortfolioService.Value;
        public IAuthService AuthService => _AuthService.Value;
        public IServicesWorkService ServicesWorkService => _ServicesWorkService.Value;
        public IClientService ClientService => _clientService.Value;
        public IWorkerService WorkerService => _workerService.Value;
        public IOrderService OrderService => _orderService.Value;
        public IRatingReviewService RatingReviewService => _ratingReviewService.Value;
        public IPaymentService PaymentService => _paymentService.Value;
        public IImageService ImageService => _imageService.Value;
    }
}