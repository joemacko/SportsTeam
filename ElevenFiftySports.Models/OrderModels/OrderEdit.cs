using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.OrderModels
{
    public class OrderEdit
    {
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
