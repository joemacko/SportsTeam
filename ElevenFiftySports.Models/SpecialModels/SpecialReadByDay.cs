using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.SpecialModels
{
    public class SpecialReadByDay
    {
        public int SpecialId { get; set; }

        public int ProductId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public double ProductSpecialPrice { get; set; }
    }
}
