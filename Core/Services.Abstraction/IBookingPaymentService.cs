using Shared;

namespace Services.Abstraction
{
    public interface IBookingPaymentService
    {
        Task<BookingDto> CreateOrUpdatePaymentIntentAsync(Guid? bookingId);
        Task UpdatePaymentStatusAsync(string requestBody, string stripeHeader);
    }
}
