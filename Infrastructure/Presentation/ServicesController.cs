using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.Dtos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/Services
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ServiceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var designs = await _serviceManager.ServicesWorkService.GetAllAsync();
            return Ok(designs);
        }

        // GET: api/Services/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var design = await _serviceManager.ServicesWorkService.GetByIdAsync(id);
            if (design == null)
                return NotFound(new { message = $"Services with ID {id} not found." });

            return Ok(design);
        }

        // POST: api/Services
        [HttpPost]
        [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromForm] CreateServicesDto dto,
            IFormFile? mainImageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (mainImageFile is not null)
            {
                var mainImagePath = await _serviceManager.ImageService.UploadImageAsync(mainImageFile, "ServiceImages");
                dto.ImageUrl = $"{Request.Scheme}://{Request.Host}{mainImagePath}";
            }

            var id = await _serviceManager.ServicesWorkService.CreateAsync(dto);
            var design = await _serviceManager.ServicesWorkService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, design);
        }

        // PUT: api/DesignTemplate/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromForm] UpdateServiceDto dto,
            IFormFile? mainImageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _serviceManager.ServicesWorkService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Services with ID {id} not found." });

            if (mainImageFile is not null)
            {
                var mainImagePath = await _serviceManager.ImageService.UploadImageAsync(mainImageFile, "ServiceImages");
                dto.ImageUrl = $"{Request.Scheme}://{Request.Host}{mainImagePath}";
            }

            await _serviceManager.ServicesWorkService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/DesignTemplate/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _serviceManager.ServicesWorkService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Services with ID {id} not found." });

            await _serviceManager.ServicesWorkService.DeleteAsync(id);
            return NoContent();
        }
    }
}
