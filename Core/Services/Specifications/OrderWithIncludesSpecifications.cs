using Domain;
using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class OrderWithIncludesSpecifications: Specifications<Order>
    {
        // get specific order by id
        public OrderWithIncludesSpecifications(Guid id):base(o=>o.Id==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.orderItems);
            
        }
        // get all orders by email[collection]
        public OrderWithIncludesSpecifications(string email):base(o=>o.UserEmail==email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.orderItems);
            AddInclude(o=>o.OrderDate);
        }
    }
}
