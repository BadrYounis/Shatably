using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IServiceManager serviceManager) : ControllerBase
    {
        // POST: api/payment/{basketId}
        [HttpPost("{basketId}")]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            if (string.IsNullOrWhiteSpace(basketId))
                return BadRequest(new { message = "Basket ID must be provided." });

            var result = await serviceManager.PaymentService.CreateOrUpdatePaymnetIntentAsync(basketId);

            if (result is null)
                return NotFound(new { message = $"Basket with ID '{basketId}' not found or invalid." });

            return Ok(result);
        }

        [HttpPost("webHook")]
        public async Task<ActionResult> webHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeHeaders = Request.Headers["Stripe-Signature"];
            await serviceManager.PaymentService.UpdatePaymnetStatus(stripeHeaders!, json);
            return new EmptyResult();
        }
    }

}

