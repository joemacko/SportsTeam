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
        public ProductType TypeOfProduct { get; set; }

        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public virtual List<Special> ProductSpecials { get; set; } = new List<Special>();

        public bool IsSpecial
        {
            get
            {
                if (ProductSpecials.Count > 0) //the ProductSpecials list will only have specials on it if the product IS a special
                    return true;

                return false;
            }
        }

    }
}
