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
        public string ProductName { get; set; }

        [Required]
        public int UnitCount { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
<<<<<<< HEAD
        public ProductType GetProductType { get; set; }

        //add virtual, did not new up (as done in restaurantrater)
        public virtual List<OrderDetail> OrderDetails { get; set; }
=======
        public ProductType TypeOfProduct { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
>>>>>>> 8082adb84ba2b850646cb959da84d760ba7b1c31
        public virtual List<Special> ProductSpecials { get; set; } = new List<Special>();

        public bool IsSpecial //we might not need this because the logic of productspecials.count > 0 could just be used in the service
        {
            get
            {
<<<<<<< HEAD
                foreach (Special special in ProductSpecials)
                {
                    if (ProductId == special.ProductId)
                    {
                        return true;
                    }
                }
=======
                if (ProductSpecials.Count > 0) //the ProductSpecials list will only have specials on it if the product IS a special
                    return true;

>>>>>>> 8082adb84ba2b850646cb959da84d760ba7b1c31
                return false;
            }
        }

    }
}
