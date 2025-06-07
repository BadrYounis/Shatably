using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Services.Abstraction;

namespace Services
{
    public class ImageService(IWebHostEnvironment _env) : IImageService
    {
        public async Task<string> UploadImageAsync(IFormFile file, string folderName = "Images")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folderName);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/{folderName}/{fileName}"; // Ex: /Images/abc123.jpg
        }
    }
}
