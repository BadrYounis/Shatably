using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IPaymentService
    {
        //Create or Update PaymentIntent
        public Task<BasketDto> CreateOrUpdatePaymnetIntentAsync(string basketId);
        public Task UpdatePaymnetStatus(string request, string stripeHeaders);

    }
}
