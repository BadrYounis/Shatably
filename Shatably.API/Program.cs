using Microsoft.Extensions.FileProviders;
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

            app.Map("/FileManager/GetImage", app =>
            {
                app.Run(async context =>
                {
                    var fileName = context.Request.Query["ImageName"];

                    // Get the file path based on the file name
                    var filePath = Path.Combine(Environment.CurrentDirectory, "Images", fileName!);

                    // Check if the file exists
                    if (File.Exists(filePath))
                    {
                        // Determine the content type based on the file extension
                        var contentType = GetContentType(Path.GetExtension(filePath));

                        // Serve the file with appropriate content type
                        context.Response.ContentType = contentType;
                        await context.Response.SendFileAsync(filePath);
                    }
                    else
                    {
                        // Return 404 if the file does not exist
                        context.Response.StatusCode = 404;
                    }
                });
            });

            string GetContentType(string fileExtension)
            {
                switch (fileExtension.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        return "image/jpeg";
                    case ".png":
                        return "image/png";
                    // Add more cases for other image types if needed
                    default:
                        return "application/octet-stream"; // Default to binary data
                }
            }

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
