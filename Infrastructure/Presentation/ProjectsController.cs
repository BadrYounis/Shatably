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

        [HttpPost] // POST: api/projects
        [ProducesResponseType(typeof(ProjectPortfolioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProjectPortfolioDto dto)
        {
            var id = await _serviceManager.ProjectPortfolioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")] // PUT: api/projects/{id}
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectPortfolioDto dto)
        {
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
