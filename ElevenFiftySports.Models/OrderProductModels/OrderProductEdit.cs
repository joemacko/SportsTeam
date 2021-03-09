using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.OrderProductModels
{
    public class OrderProductEdit
    {
        [Required, Range(1, Int32.MaxValue)]
        public int OrderId { get; set; }

        [Required, Range(1, Int32.MaxValue)]
        public int ProductId { get; set; }

        [Required, Range(1, Int32.MaxValue)]
        public int ProductCount { get; set; }
    }
}
