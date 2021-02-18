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
        public string ProductName { get; set; }
        public int UnitCount { get; set; }
        public double ProductPrice { get; set; }
        public ProductType TypeOfProduct { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>(); //add virtual, did not new up (as done in restaurantrater)
        public virtual List<Special> ProductSpecials { get; set; } = new List<Special>();

        public bool IsSpecial
        {
            get
            {
                foreach (Special special in ProductSpecials)
                {
                    if (ProductId == special.ProductId)
                    {
                        return true;
                    }
                }
                return false;

            }
        }

    }
}
