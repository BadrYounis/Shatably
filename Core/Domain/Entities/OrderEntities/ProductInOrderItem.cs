using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class ProductInOrderItem
    {
        public ProductInOrderItem()
        {
            
        }
        public ProductInOrderItem(Guid productId, string productName, string imageUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ImageUrl = imageUrl;
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
    }
}
