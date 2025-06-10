using Services.Abstraction;
using Services;
using Shared;
using Persistence.Managers;
using Domain;

namespace Shatably.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IProjectPortfolioService, ProjectPortfolioService>();
            services.AddScoped<IServicesWorkService, ServicesWorkService>();
            //Users
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            services.Configure<EmailService>(configuration.GetSection("EmailSettings"));
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
