using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class RatingReviewDto
    {
        public Guid ReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ClientId { get; set; }
        public Guid? WorkerId { get; set; }
        public Guid? ProjectPortfolioId { get; set; }

        // to know the name of project or worker 
        public string? WorkerName { get; set; }
        public string? ProjectTitle { get; set; }
    }
}
