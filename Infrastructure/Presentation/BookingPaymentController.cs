using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingPaymentController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("{bookingId}")]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingDto>> CreateOrUpdatePaymentIntent(Guid? bookingId)
        {
            if (bookingId is null)
                return BadRequest(new { message = "Booking ID must be provided." });

            var result = await _serviceManager.BookingPaymentService.CreateOrUpdatePaymentIntentAsync(bookingId);

            if (result is null)
                return NotFound(new { message = $"Booking with ID '{bookingId}' not found or invalid." });

            return Ok(result);
        }

        [HttpPost("bookingWebHook")]
        public async Task<ActionResult> BookingWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeHeaders = Request.Headers["Stripe-Signature"];
            await _serviceManager.BookingPaymentService.UpdatePaymentStatusAsync(stripeHeaders!, json);
            return new EmptyResult();           
        }
    }
}
