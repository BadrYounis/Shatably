using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ServicesWorkNotFoundException : NotFoundException
    {
        public ServicesWorkNotFoundException(Guid id) : base($"Service With Id: {id} Not Found")
        {

        }
    }
}
