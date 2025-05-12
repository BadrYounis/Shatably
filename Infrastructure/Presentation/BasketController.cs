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
        public async Task<ActionResult<BasketDto>> GetBasket(string username)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(username);
            return Ok(basket);
        }

        // POST: api/Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> UpdateBasket([FromBody] BasketDto basketDto)
        {
            var updatedBasket = await _serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(updatedBasket);
        }

        // DELETE: api/Basket/{username}
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(username);
            return result ? NoContent() : NotFound();
        }
    }
}
