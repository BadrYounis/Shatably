using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
   public class OrderResult
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; }
        public AddressDto ShippingAddress { get; init; }
        //order ==> sandwich [orderItem1] - coffee[orderItem2] - chocolate[orderItem3] 
        public ICollection<OrderItemDTO> orderItems { get; init; } = new List<OrderItemDTO>();
        public string PaymentStatus { get; init; }

        // get only the name of DeliveryMethod
        public string DeliveryMethod { get; init; }= "Standard Shipping";
        public decimal SubTotal { get; init; } // orderItem.Price * orderItem.Quantity // Total ==> SubTotal + ShippingPrice
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; init; } = string.Empty;
        public decimal Total { get; init; }
    }
}
