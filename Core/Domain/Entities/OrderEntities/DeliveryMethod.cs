using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class DeliveryMethod 
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, decimal price, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Price = price;
            DeliveryTime = deliveryTime;
        }
        public Guid Id { get; set; }
        public string ShortName {  get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; } //within a day
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
