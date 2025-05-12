using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProjectPortfolioNotFoundException : NotFoundException
    {
        public ProjectPortfolioNotFoundException(Guid id) : base($"Project With Id: {id} Not Found")
        {
        }
    }
}
