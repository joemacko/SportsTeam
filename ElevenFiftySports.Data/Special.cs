using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; } // btw, 0 is sunday, 1 is monday and so on

        [Required]
        public double ProductSpecialPrice { get; set; } // Dollar amount
    }
}
