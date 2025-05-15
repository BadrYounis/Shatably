using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public class DeliveryMethodNotFoundException:NotFoundException
    {
        public DeliveryMethodNotFoundException(Guid id) : base($"The Delivery Method With ID : {id} IS NOT FOUND !! ") { }

    }
}
