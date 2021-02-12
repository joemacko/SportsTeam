using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderDetail> OrderDetails {get; set;}
        // public List<Product> Products { get; set; } //remove to match reference https://dev.to/_patrickgod/many-to-many-relationship-with-entity-framework-core-4059
        public double TotalCost { get; set; }
    }
}
