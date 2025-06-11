using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class UpdateRatingReviewDto
    {
        [Required]
        public Guid ReviewId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public double Rating { get; set; }
        public string? Comment { get; set; }
    }
}
