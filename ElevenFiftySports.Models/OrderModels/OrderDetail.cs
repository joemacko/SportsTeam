using ElevenFiftySports.Data;
using ElevenFiftySports.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.OrderModels
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public List<OrderProductListItem> OrderProducts = new List<OrderProductListItem>();  
        public double TotalCost;
        public DateTimeOffset CreatedOrderDate { get; set; }

    }
}
