using Shared;

namespace Services.Abstraction
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(BookingCreateDto dto);
        Task<BookingDto> UpdateBookingAsync(BookingUpdateDto dto);
        Task DeleteBookingAsync(Guid bookingId);
        Task<BookingDto> GetBookingByIdAsync(Guid bookingId);
        Task<IEnumerable<BookingDto>> GetAllBookingsForClientAsync(Guid clientId);
        Task<IEnumerable<BookingDto>> GetAllBookingsForWorkerAsync(Guid workerId);
    }
}
