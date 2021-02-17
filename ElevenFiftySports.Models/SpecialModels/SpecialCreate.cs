using ElevenFiftySports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.SpecialModels
{
    public class SpecialCreate
    {
        public int SpecialId { get; set; }

        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int ProductId { get; set; }

        public DateTime DayOfWeek { get; set; }

        public double ProductSpecialPrice { get; set; }
    }
}
