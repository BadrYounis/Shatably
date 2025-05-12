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
    public class DesignTemplateController(IServiceManager _serviceManager): ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DesignTemplateDto>>> GetAll([FromQuery] DesignTemplateSpecificationsParameters parameters)
        {
            var designs = await _serviceManager.DesignTemplateService.GetAllAsync(parameters);
            return Ok(designs);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<DesignTemplateDto>> GetById(Guid id)
        {
            var designs = await _serviceManager.DesignTemplateService.GetByIdAsync(id);
            return Ok(designs);
        }

        
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var designs = await _serviceManager.DesignTemplateService.GetByCategoryAsync(category);
            return Ok(designs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDesignTemplateDto dto)
        {
            var id = await _serviceManager.DesignTemplateService.CreateAsync(dto);
            var design = await _serviceManager.DesignTemplateService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, design);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDesignTemplateDto dto)
        {
            await _serviceManager.DesignTemplateService.UpdateAsync(id, dto);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceManager.DesignTemplateService.DeleteAsync(id);
            return NoContent();
        }

    }
}
