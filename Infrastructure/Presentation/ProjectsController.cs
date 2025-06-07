using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] // GET: api/projects
        [ProducesResponseType(typeof(IEnumerable<ProjectPortfolioDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectPortfolioDto>>> GetAll([FromQuery] ProjectSpecificationsParameters parameters)
        {
            var result = await _serviceManager.ProjectPortfolioService.GetAllAsync(parameters);
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: api/projects/{id}
        [ProducesResponseType(typeof(ProjectPortfolioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceManager.ProjectPortfolioService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ProjectPortfolioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromForm] CreateProjectPortfolioDto dto,
             IFormFile? mainImageFile,
             List<IFormFile>? galleryImages)
        {
            if (mainImageFile is not null)
            {
                var mainImagePath = await _serviceManager.ImageService.UploadImageAsync(mainImageFile, "ProjectImages");
                dto.mainImageUrl = $"{Request.Scheme}://{Request.Host}{mainImagePath}";
            }

            if (galleryImages is not null && galleryImages.Count > 0)
            {
                dto.ImageUrls = new HashSet<string>();

                foreach (var file in galleryImages)
                {
                    var imagePath = await _serviceManager.ImageService.UploadImageAsync(file, "ProjectImages");
                    dto.ImageUrls.Add($"{Request.Scheme}://{Request.Host}{imagePath}");
                }
            }

            var id = await _serviceManager.ProjectPortfolioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromForm] UpdateProjectPortfolioDto dto,
            IFormFile? mainImageFile,
            List<IFormFile>? galleryImages)
        {
            if (mainImageFile is not null)
            {
                var mainImagePath = await _serviceManager.ImageService.UploadImageAsync(mainImageFile, "ProjectImages");
                dto.ImageUrl = $"{Request.Scheme}://{Request.Host}{mainImagePath}";
            }

            if (galleryImages is not null && galleryImages.Count > 0)
            {
                dto.ImageUrls = new HashSet<string>();

                foreach (var file in galleryImages)
                {
                    var imagePath = await _serviceManager.ImageService.UploadImageAsync(file, "ProjectImages");
                    dto.ImageUrls.Add($"{Request.Scheme}://{Request.Host}{imagePath}");
                }
            }

            await _serviceManager.ProjectPortfolioService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")] // DELETE: api/projects/{id}
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceManager.ProjectPortfolioService.DeleteAsync(id);
            return NoContent();
        }
    }
}
