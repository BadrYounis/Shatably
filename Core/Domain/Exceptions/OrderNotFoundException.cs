using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderNotFoundException:Exception
    {
        public OrderNotFoundException(Guid id) : base($"The Order With ID : {id} IS NOT FOUND !! ") { }

    }
}
