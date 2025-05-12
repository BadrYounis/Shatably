using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
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
        public ServiceManager(IUnitOfWork unitOfWork, IBasketRepository _basketRepository, UserManager<User> _userManager,
            IOptions<JwtOptions> _options, AppDbContext _context, IEmailService _emailService
            , IHttpContextAccessor _httpContextAccessor, LinkGenerator _linkGenerator)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork));
            _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository));
            _projectPortfolioService = new Lazy<IProjectPortfolioService>(() => new ProjectPortfolioService(unitOfWork));
            _AuthService = new Lazy<IAuthService>(() => new AuthService(_userManager, _options, _emailService, _httpContextAccessor, _linkGenerator));
        }
        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;
        public IProjectPortfolioService ProjectPortfolioService => _projectPortfolioService.Value;
        public IAuthService AuthService => _AuthService.Value;
    }
}
