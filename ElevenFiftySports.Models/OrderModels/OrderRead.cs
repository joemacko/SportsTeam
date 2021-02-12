using ElevenFiftySports.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models
{
    public class OrderRead
    {
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public double TotalCost { get; set; }
    }
}
