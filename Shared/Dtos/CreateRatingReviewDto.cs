using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class CreateRatingReviewDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string? Comment { get; set; }

        [Required(ErrorMessage = "ClientId is required.")]
        public Guid ClientId { get; set; }
        // واحد فقط منهم لازم يكون موجود
        public Guid? WorkerId { get; set; }
        public Guid? ProjectPortfolioId { get; set; }
    }
}
