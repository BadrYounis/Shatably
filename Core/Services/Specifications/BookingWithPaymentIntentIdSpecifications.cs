using Domain.Contracts;

namespace Services.Specifications
{
    public class BookingWithPaymentIntentIdSpecifications : Specifications<ServiceBooking>
    {
        public BookingWithPaymentIntentIdSpecifications(string paymnetIntentId) : base(o => o.PaymentIntentId == paymnetIntentId)
        {

        }
    }
}
