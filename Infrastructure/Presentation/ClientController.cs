using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(IServiceManager _serviceManager) : ControllerBase
    {
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
