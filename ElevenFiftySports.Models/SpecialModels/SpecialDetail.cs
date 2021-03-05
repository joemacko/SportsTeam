using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.SpecialModels
{
    public class SpecialDetail
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter a correct Special ID.")]
        public int SpecialId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter a correct Product ID.")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Product Name should contain at least two characters.")]
        public string ProductName { get; set; }

        [Required]
        public string DayOfWeek { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "The special price must be greater than 0.")]
        public double ProductSpecialPrice { get; set; }
    }
}
