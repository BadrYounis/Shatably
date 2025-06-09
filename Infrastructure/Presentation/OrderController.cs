using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.OrderModels;
using System.Security.Claims;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager serviceManager) : ControllerBase
    {
        // POST: api/order
        [HttpPost]
        [ProducesResponseType(typeof(OrderResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] OrderRequest orderRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null)
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "User email not found in token." });

            var order = await serviceManager.OrderService.CreateOrderAsync(orderRequest, email);
            if (order is null)
                return BadRequest(new { message = "Order creation failed." });

            return Ok(order);
        }

        // GET: api/order
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderRequest>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null)
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "User email not found in token." });

            var orders = await serviceManager.OrderService.GetAllOrdersByEmailAsync(email);
            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            if (order is null)
                return NotFound(new { message = $"Order with ID {id} not found." });

            return Ok(order);
        }

      
    }
}
