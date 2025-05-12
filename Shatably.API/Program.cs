using Shatably.API.Extensions;

namespace Shatably.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Configure Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Presentation Services
            builder.Services.AddPresentationServices();

            //Core Services
            builder.Services.AddCoreServices(builder.Configuration);

            //Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration); 
            #endregion

            var app = builder.Build();

            #region Configuring Kestrel Middlewares

            app.UseCustomExceptionMiddleware();

            await app.SeedDbAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(); 

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();

        }
    }
}
