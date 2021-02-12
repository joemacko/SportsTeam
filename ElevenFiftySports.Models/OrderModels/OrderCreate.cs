using ElevenFiftySports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.OrderModels
{
    public class OrderCreate
    {
        [Required]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
