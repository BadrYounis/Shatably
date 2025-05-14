using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkersController(IServiceManager _serviceManager) : ControllerBase
    {
        //GET: BaseUrl/api/Workers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workers = await _serviceManager.WorkerService.GetAllAsync();
            return Ok(workers);
        }

        //GET: BaseUrl/api/Workers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var worker = await _serviceManager.WorkerService.GetByIdAsync(id);
            if (worker == null)
                return NotFound();

            return Ok(worker);
        }

        //GET: BaseUrl/api/Workers/type/{type}
        [HttpGet("type/{type}")]
        [ProducesResponseType(typeof(IEnumerable<WorkerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByType(string type)
        {
            try
            {
                var workers = await _serviceManager.WorkerService.GetByTypeAsync(type);
                if (!workers.Any())
                    return Ok(new { message = $"No workers found of type: {type}", data = workers });

                return Ok(new { message = "Workers retrieved successfully", data = workers });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ... existing code ...
        //POST: BaseUrl/api/Workers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateWorkerDto dto)
        {
            try
            {
                var id = await _serviceManager.WorkerService.CreateAsync(dto);
                var created = await _serviceManager.WorkerService.GetByIdAsync(id);

                if (created == null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { message = "Worker was created but could not be retrieved." });

                return CreatedAtAction(nameof(GetById), new { id = created.WorkerId }, new
                {
                    message = "Worker created successfully.",
                    data = created
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //PUT: BaseUrl/api/Workers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWorkerDto dto)
        {
            await _serviceManager.WorkerService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: BaseUrl/api/Workers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceManager.WorkerService.DeleteAsync(id);
            return NoContent();
        }      
    }
}
