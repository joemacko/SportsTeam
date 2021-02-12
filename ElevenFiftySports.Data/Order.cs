using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Data
{
    public class Order
    {
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public List<Product> Products { get; set; }
        public double TotalCost { get; set; }
    }
}
