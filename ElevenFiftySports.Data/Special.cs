using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Data
{
    public class Special
    {
        [Key]
        public int SpecialId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime DayOfWeek { get; set; }

        [Required]
        public double ProductSpecialPrice { get; set; }
    }
}
