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
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public int UnitCount { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than 0.00.")]
        public double ProductPrice { get; set; }

        [Required]
        public ProductType GetProductType { get; set; }

        public ProductType TypeOfProduct { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; } //add virtual, did not new up (as done in restaurantrater)
    }
}
