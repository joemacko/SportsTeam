using ElevenFiftySports.Data;
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
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public double TotalCost;
    }
}
