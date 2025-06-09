using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/Basket/{username}
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBasket(string username)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(username);

            if (basket == null)
                return NotFound(new { message = $"Basket not found for user: {username}" });

            return Ok(basket);
        }

        // POST: api/Basket
        [HttpPost]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBasket ([FromBody] BasketDto basketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);          

            var updatedBasket = await _serviceManager.BasketService.UpdateBasketAsync(basketDto);

            if (updatedBasket == null)
                return BadRequest(new { message = "Failed to update basket." });

            return Ok(updatedBasket);
        }

        // DELETE: api/Basket/{username}
        [HttpDelete("{username}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(username);

            if (!result)
                return NotFound(new { message = $"No basket found for user: {username}" });

            return NoContent();
        }
    }
}
