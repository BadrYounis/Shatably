using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Services.Specification;
using Services.Specifications;
using Shared;
using Stripe;

namespace Services
{
    public class BookingPaymentService : IBookingPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public BookingPaymentService(IUnitOfWork unitOfWork, IConfiguration configuration) {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<BookingDto> CreateOrUpdatePaymentIntentAsync(Guid? bookingId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

            var bookingRepo = _unitOfWork.GetRepository<ServiceBooking, Guid>();

            var booking = await bookingRepo.GetByIdAsync(bookingId!.Value)
                         ?? throw new NotFoundException($"{bookingId} is not found");

            var price = booking.TotalPrice; // Assume it's already calculated
            var amount = (long)(price * 100);

            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(booking.PaymentIntentId))
            {
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var paymentIntent = await service.CreateAsync(createOptions);
                booking.PaymentIntentId = paymentIntent.Id;
                booking.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(booking.PaymentIntentId, updateOptions);
            }

            bookingRepo.Update(booking);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(booking);
        }
        public async Task UpdatePaymentStatusAsync(string requestBody, string stripeHeader)
        {
            var endPointSecret = _configuration.GetSection("StripeSettings")["EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(requestBody,
                stripeHeader, endPointSecret, throwOnApiVersionMismatch: false);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentStatusSucceeded(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentStatusFailed(paymentIntent.Id);

            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }
        private async Task UpdatePaymentStatusFailed(string PaymentIntentid)
        {
            var bookingRepo = _unitOfWork.GetRepository<ServiceBooking, Guid>();
            var booking = await bookingRepo.GetByIdAsync(new BookingWithPaymentIntentIdSpecifications(PaymentIntentid)) ?? throw new Exception();
            booking.PaymentStatus = BookingPaymentStatus.PaymentFailed;
            bookingRepo.Update(booking);
            await _unitOfWork.SaveChangesAsync();
        }
        private async Task UpdatePaymentStatusSucceeded(string PaymentIntentid)
        {
            var bookingRepo = _unitOfWork.GetRepository<ServiceBooking, Guid>();
            var booking = await bookingRepo.GetByIdAsync(new BookingWithPaymentIntentIdSpecifications(PaymentIntentid)) ?? throw new Exception();
            booking.PaymentStatus = BookingPaymentStatus.PaymentReceived;
            bookingRepo.Update(booking);
            await _unitOfWork.SaveChangesAsync();
        }
        private static BookingDto MapToDto(ServiceBooking booking) => new BookingDto
        {
            BookingId = booking.BookingId,
            ClientId = booking.ClientId,
            BookingDate = booking.BookingDate,
            TotalPrice = booking.TotalPrice,
            PaymentIntentId = booking.PaymentIntentId,
            ClientSecret = booking.ClientSecret
        };
    }
}
