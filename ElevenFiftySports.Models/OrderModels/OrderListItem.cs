using ElevenFiftySports.Data;
using ElevenFiftySports.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models
{
    public class OrderListItem
    {
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public List<OrderProductListItem> OrderProducts = new List<OrderProductListItem>(); // cannot be a class (list<orderproduct>) because you cannot send a raw data class to postman (which is why the model is utilized)
        public double TotalCost { get; set; }
        public DateTimeOffset CreatedOrderDate { get; set; }
    }
}
