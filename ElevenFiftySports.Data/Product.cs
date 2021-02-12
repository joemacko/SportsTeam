using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Data
{
    public enum ProductType
    {
        Food = 1,
        Drink
    }
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int UnitCount { get; set; }
        public double ProductPrice { get; set; }
        public ProductType TypeOfProduct { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
