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
    public class DesignTemplateController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/DesignTemplate
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DesignTemplateDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] DesignTemplateSpecificationsParameters parameters)
        {
            var designs = await _serviceManager.DesignTemplateService.GetAllAsync(parameters);
            return Ok(designs);
        }

        // GET: api/DesignTemplate/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DesignTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var design = await _serviceManager.DesignTemplateService.GetByIdAsync(id);
            if (design == null)
                return NotFound(new { message = $"Design template with ID {id} not found." });

            return Ok(design);
        }

        // GET: api/DesignTemplate/category/{category}
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(IEnumerable<DesignTemplateDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var designs = await _serviceManager.DesignTemplateService.GetByCategoryAsync(category);
            return Ok(designs);
        }

        // POST: api/DesignTemplate
        [HttpPost]
        [ProducesResponseType(typeof(DesignTemplateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromForm] CreateDesignTemplateDto dto,
            IFormFile? mainImageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (mainImageFile is not null)
            {
                var mainImagePath = await _serviceManager.ImageService.UploadImageAsync(mainImageFile, "DesignImages");
                dto.imageUrl = $"{Request.Scheme}://{Request.Host}{mainImagePath}";
            }

            var id = await _serviceManager.DesignTemplateService.CreateAsync(dto);
            var design = await _serviceManager.DesignTemplateService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, design);
        }

        // PUT: api/DesignTemplate/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromForm] UpdateDesignTemplateDto dto,
            IFormFile? mainImageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _serviceManager.DesignTemplateService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Design template with ID {id} not found." });

            if (mainImageFile is not null)
            {
                var mainImagePath = await _serviceManager.ImageService.UploadImageAsync(mainImageFile, "DesignImages");
                dto.imageUrl = $"{Request.Scheme}://{Request.Host}{mainImagePath}";
            }

            await _serviceManager.DesignTemplateService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/DesignTemplate/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _serviceManager.DesignTemplateService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Design template with ID {id} not found." });

            await _serviceManager.DesignTemplateService.DeleteAsync(id);
            return NoContent();
        }
    }
}
