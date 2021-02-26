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

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int UnitCount { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        public ProductType GetProductType { get; set; }

        //add virtual, did not new up (as done in restaurantrater)
        public virtual List<OrderDetail> OrderDetails { get; set; }
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
