using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderItemDTO
    {
        #region Attributes of ProductInOrderItem 
        // we need dto to be flat [ not complex ]
        public Guid ProductId { get; init; }
        public string ProductName { get; init; }
        public string PictureUrl { get; init; } 

        #endregion
        public int Quantity { get; init; }
        public decimal Price { get; init; }
    }
}
