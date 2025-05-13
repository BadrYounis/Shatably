using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using StackExchange.Redis;

namespace Shatably.API.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Business
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Identity
            services.AddDbContext<IdentityAppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));
            });
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityAppDbContext>()
              .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            //Basket
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configOptions = new ConfigurationOptions
                {
                    EndPoints = { "better-amoeba-29825.upstash.io:6379" },
                    Password = "AXSBAAIjcDE2YTgyYTIzZDdiZGM0Y2I4OTc3M2MzYWIyNWRkYzBlY3AxMA",
                    User = "default",
                    Ssl = true,
                    AbortOnConnectFail = false
                };
                return ConnectionMultiplexer.Connect(configOptions);
            });
            return services;
        }

    }
}
