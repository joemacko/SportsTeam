using ElevenFiftySports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.SpecialModels
{
    public class SpecialEdit
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter a correct Special ID.")]
        public int SpecialId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter a correct Product ID.")]
        public int ProductId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "The special price must be greater than 0.")]
        public double ProductSpecialPrice { get; set; }
    }
}
