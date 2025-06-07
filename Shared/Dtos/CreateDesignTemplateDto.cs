using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
   public class CreateDesignTemplateDto
    {
        [Required(ErrorMessage = "Design name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Design is required.")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Design description is required.")]
        [StringLength(200, ErrorMessage = "Product description cannot exceed 200 characters.")]
        public string Description { get; set; }
        public string? imageUrl { get; set; }

        //[Required(ErrorMessage = "Price is required.")]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        //public decimal Price { get; set; }

        //[Required(ErrorMessage = "Stock amount is required.")]
        //[Range(0, int.MaxValue, ErrorMessage = "Stock amount cannot be negative.")]
        //public int StockAmount { get; set; }

    }
}
