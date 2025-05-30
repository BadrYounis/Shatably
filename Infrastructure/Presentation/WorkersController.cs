using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkersController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/Workers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkerDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var workers = await _serviceManager.WorkerService.GetAllAsync();
            return Ok(workers);
        }

        // GET: api/Workers/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var worker = await _serviceManager.WorkerService.GetByIdAsync(id);
            if (worker == null)
                return NotFound(new { message = $"Worker with id {id} not found." });

            return Ok(worker);
        }

        // GET: api/Workers/type/{type}
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

        // POST: api/Workers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateWorkerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

        // PUT: api/Workers/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWorkerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _serviceManager.WorkerService.ExistsAsync(id);
            if (!exists)
                return NotFound(new { message = $"Worker with id {id} not found." });

            await _serviceManager.WorkerService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/Workers/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exists = await _serviceManager.WorkerService.ExistsAsync(id);
            if (!exists)
                return NotFound(new { message = $"Worker with id {id} not found." });

            await _serviceManager.WorkerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
