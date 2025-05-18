using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class OrderWithPaymentIntentIdSpecifications : Specifications<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymnetIntentId) : base(o => o.PaymentIntentId == paymnetIntentId)
        {

        }
    }
}
