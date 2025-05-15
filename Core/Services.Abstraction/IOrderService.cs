using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        // Get Order By Id ==> OrderResult (Guid Id)
        Task<OrderResult> GetOrderByIdAsync(Guid id);
       
        // Get All Orders For Users By Email ==> IEnumrable<OrderResult> (string userEmail)
        Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail);

        // Create Order ==> orderRequest ==> (OrderRequest Req , string UserEmail)
        Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail);

        // Get All Delivery Methods ==> IEnumrable<DeliveryMethodResult>
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
