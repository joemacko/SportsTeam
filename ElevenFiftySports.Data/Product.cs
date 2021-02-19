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

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public int UnitCount { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than 0.00.")]
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
