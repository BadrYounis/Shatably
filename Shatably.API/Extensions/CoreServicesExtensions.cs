using Services.Abstraction;
using Services;
using Shared;
using Persistence.Managers;

namespace Shatably.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
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
