using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class DesignTemplateDto
    {
        public Guid templateId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string imageUrl { get; set; }
        public string description { get; set; }
    }
}
