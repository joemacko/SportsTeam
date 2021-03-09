using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.OrderProductModels
{
    public class OrderProductListItem 
    {
        [Required]
        public int PrimaryId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string CustomerFirstName { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int ProductCount { get; set; }
    }
}
