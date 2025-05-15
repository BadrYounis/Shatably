using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class RatingReviewDto
    {

        public int Rating { get; set; }           // Rating (e.g., from 1 to 5)
        public string? Comment { get; set; }       // Optional: client feedback
        public DateTime CreatedAt { get; set; }
    }
}
