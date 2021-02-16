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
        public int PrimaryId { get; set; } //adding primary id to keep a count of how many of each product?                           ex. PId = 1 OId =1 PrId =2; PId = 1 OId = 1 PrId = 2, would mean 2 orders of product 2 on orderid 1 ???
        [Key]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Key]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int ProductCount { get; set; } //
    }
}
