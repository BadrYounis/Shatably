using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
        {
            var result = await _serviceManager.BookingService.CreateBookingAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingUpdateDto dto)
        {
            var result = await _serviceManager.BookingService.UpdateBookingAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            await _serviceManager.BookingService.DeleteBookingAsync(id);
            return NoContent();
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetBookingsForClient(Guid clientId)
        {
            var bookings = await _serviceManager.BookingService.GetAllBookingsForClientAsync(clientId);
            return Ok(bookings);
        }

        [HttpGet("worker/{workerId}")]
        public async Task<IActionResult> GetBookingsForWorker(Guid workerId)
        {
            var bookings = await _serviceManager.BookingService.GetAllBookingsForWorkerAsync(workerId);
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(Guid id)
        {
            var booking = await _serviceManager.BookingService.GetBookingByIdAsync(id);
            return Ok(booking);
        }
    }
}
