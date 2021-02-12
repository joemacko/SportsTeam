using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Data
{
    public class OrderDetail    
    {
        [Key]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        [Key]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
