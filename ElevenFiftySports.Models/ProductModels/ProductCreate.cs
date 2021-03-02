using ElevenFiftySports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.ProductModels
{
    public class ProductCreate
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The field must be greater than 0.")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Product Name should be a min of 2 Characters.")]
        public string ProductName { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The field must be greater than 0.")]
        public int UnitCount { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The field must be greater than 0.00.")]
        public double ProductPrice { get; set; }

        [Required]
        public ProductType GetProductType { get; set; }

    }
}

