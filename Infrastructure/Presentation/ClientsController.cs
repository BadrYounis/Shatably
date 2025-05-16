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
        public async Task<IActionResult> GetAll()
        {
            var clients = await _serviceManager.ClientService.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientResultDto>> GetById(Guid id)
        {
            var client = await _serviceManager.ClientService.GetByIdAsync(id);
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDto dto)
        {
            var id = await _serviceManager.ClientService.CreateAsync(dto);
            var client = await _serviceManager.ClientService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClientDto dto)
        {
            await _serviceManager.ClientService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceManager.ClientService.DeleteAsync(id);
            return NoContent();
        }
    }
}
