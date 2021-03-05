using ElevenFiftySports.Data;
using ElevenFiftySports.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.OrderModels
{
    public class OrderDetail
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string CustomerFirstName { get; set; }
        
        [Required]
        public List<OrderProductListItem> OrderProducts = new List<OrderProductListItem>();
        
        [Required]
        public double Cost;

        public bool OrderFinalized { get; set; }
        
        [Required]
        public DateTimeOffset CreatedOrderDate { get; set; }

    }
}
