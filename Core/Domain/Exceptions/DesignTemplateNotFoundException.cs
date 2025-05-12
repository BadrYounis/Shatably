using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DesignTemplateNotFoundException : NotFoundException
    {
        public DesignTemplateNotFoundException(Guid id) : base($"Design With Id: {id} Not Found")
        {

        }
    }
}
