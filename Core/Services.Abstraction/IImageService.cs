using Microsoft.AspNetCore.Http;

namespace Services.Abstraction
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile file, string folderName = "Images");
    }
}
