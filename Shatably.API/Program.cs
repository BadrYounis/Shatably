using Shatably.API.Extensions;

namespace Shatably.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Configure Services
            var builder = WebApplication.CreateBuilder(args);

            // CORS Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add services to the container.

            //Presentation Services
            builder.Services.AddPresentationServices();

            //Core Services
            builder.Services.AddCoreServices(builder.Configuration);

            //Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //Security Services
            builder.Services.AddJwtAuthentication(builder.Configuration);
            #endregion

            var app = builder.Build();

            #region Configuring Kestrel Middlewares

            app.UseCustomExceptionMiddleware();

            // Enable CORS
            app.UseCors("AllowAll");

            await app.SeedDbAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();

        }
    }
}
