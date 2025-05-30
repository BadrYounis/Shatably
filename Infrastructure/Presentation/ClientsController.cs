using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/Clients
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientResultDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _serviceManager.ClientService.GetAllAsync();
            return Ok(clients);
        }

        // GET: api/Clients/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _serviceManager.ClientService.GetByIdAsync(id);
            if (client == null)
                return NotFound(new { message = $"Client with ID {id} not found." });

            return Ok(client);
        }

        // POST: api/Clients
        [HttpPost]
        [ProducesResponseType(typeof(ClientResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateClientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _serviceManager.ClientService.CreateAsync(dto);
            var client = await _serviceManager.ClientService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, client);
        }

        // PUT: api/Clients/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _serviceManager.ClientService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Client with ID {id} not found." });

            await _serviceManager.ClientService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/Clients/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _serviceManager.ClientService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Client with ID {id} not found." });

            await _serviceManager.ClientService.DeleteAsync(id);
            return NoContent();
        }
    }
}
