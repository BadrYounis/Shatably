
namespace Domain.Entities.OrderEntities
{
    public class Order 
    {
        public Order()
        {
            
        }
        public Order(string userEmail,
            Address shippingAddress,
            ICollection<OrderItem> orderItems,
            DeliveryMethod deliveryMethod,
            decimal subTotal,
            string paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            this.orderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public Address ShippingAddress { get; set; }
        //order ==> sandwich [orderItem1] - coffee[orderItem2] - chocolate[orderItem3] 
        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
        public Guid? DeliveryMethodId { get; set; }
        public decimal SubTotal { get; set; } // orderItem.Price * orderItem.Quantity // Total ==> SubTotal + ShippingPrice
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; set; } 
        public Client Client { get; set; }
        public Guid ClientId { get; set; }
    }
}
