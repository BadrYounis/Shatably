using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/products
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] ProductSpecificationsParameters parameters)
        {
            var products = await _serviceManager.ProductService.GetAllAsync(parameters);
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _serviceManager.ProductService.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { message = $"Product with ID '{id}' not found." });

            return Ok(product);
        }

        // GET: api/products/category/{category}
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var products = await _serviceManager.ProductService.GetByCategoryAsync(category);
            return Ok(products);
        }

        // POST: api/products
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var id = await _serviceManager.ProductService.CreateAsync(dto);
            var created = await _serviceManager.ProductService.GetByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id }, new
            {
                message = "Product added successfully.",
                data = created
            });
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
        {
            await _serviceManager.ProductService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceManager.ProductService.DeleteAsync(id);
            return NoContent();
        }
    }
}
