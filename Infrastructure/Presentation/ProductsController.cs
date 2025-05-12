using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] ProductSpecificationsParameters parameters)
        {
            var products = await _serviceManager.ProductService.GetAllAsync(parameters);
            return Ok(products);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _serviceManager.ProductService.GetByIdAsync(id);
            return Ok(product);
        }

        // GET: api/product/category/{category}
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var products = await _serviceManager.ProductService.GetByCategoryAsync(category);
            return Ok(products);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var id = await _serviceManager.ProductService.CreateAsync(dto);
            var product = await _serviceManager.ProductService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, product);
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
        {
            await _serviceManager.ProductService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceManager.ProductService.DeleteAsync(id);
            return NoContent();
        }
    }
}