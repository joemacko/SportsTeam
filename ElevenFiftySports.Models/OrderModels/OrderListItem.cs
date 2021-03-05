using ElevenFiftySports.Data;
using ElevenFiftySports.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models
{
    public class OrderListItem
    {
        [Required]
        public int OrderId { get; set; }
        
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string CustomerFirstName { get; set; }

        [Required]
        public List<OrderProductListItem> OrderProducts = new List<OrderProductListItem>(); // cannot be a class (list<orderproduct>) because you cannot send a raw data class to postman (which is why the model is utilized)

        [Required]
        public double Cost { get; set; }

        [Required]
        public DateTimeOffset CreatedOrderDate { get; set; }

        public bool OrderFinalized { get; set; }
    }
}
