//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Services.Abstraction;

//namespace Presentation
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UploadController(IServiceManager _serviceManager) : ControllerBase
//    {
//        [HttpPost("upload-image")]
//        [Consumes("multipart/form-data")]  //"الميثود دي بتحتاج ملف يترفع باستخدام multipart/form-data، فاعرض upload UI المناسب".

//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> UploadImage(IFormFile file)
//        {
//            try
//            {
//                var url = await _serviceManager.ImageService.UploadImageAsync(file);
//                var fullUrl = $"{Request.Scheme}://{Request.Host}{url}";

//                return Ok(new { Url = fullUrl });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Error = ex.Message });
//            }
//        }
//    }
//}
