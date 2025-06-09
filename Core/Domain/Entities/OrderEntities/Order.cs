
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
            decimal subTotal,
            string paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            this.orderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
            ShippingPrice = 50m;
        }

        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public Address ShippingAddress { get; set; }
        //order ==> sandwich [orderItem1] - coffee[orderItem2] - chocolate[orderItem3] 
        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public decimal SubTotal { get; set; } // orderItem.Price * orderItem.Quantity // Total ==> SubTotal + ShippingPrice
        public decimal ShippingPrice { get; set; } = 50m;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; set; } 
        public Client Client { get; set; }
        public Guid ClientId { get; set; }
    }
}
