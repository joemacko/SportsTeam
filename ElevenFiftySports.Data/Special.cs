using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Data
{
    public class Special
    {
        [Key]
        public int SpecialId { get; set; }

        //[Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product{ get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; } // btw, 0 is sunday, 1 is monday and so on

        // Dollar amount
        [Required]
        public double ProductSpecialPrice { get; set; }
        //{
        //    get
        //    {
        //        Special special = new Special();
        //        double productSpecial = special.ProductSpecialPrice;
        //        double productSpecialPrice = 
        //        special.DayOfWeek

        //        // set products equal to a certain price or percent

        //        switch (DayOfWeek)
        //        {
        //            case 0:
        //                special.ProductId = 1;
        //                productSpecialPrice = 1 - (productSpecialPrice*0.1);
        //                break;
        //            case 1:
        //                special.ProductId = 2;
        //                productSpecial = special.Product.ProductPrice - (special.Product.ProductPrice * 0.10);
        //                break;
        //            case 2:
        //                special.ProductId = 3;
        //                productSpecial = special.Product.ProductPrice - (special.Product.ProductPrice * 0.10);
        //                break;
        //            case 3:
        //                special.ProductId = 4;
        //                productSpecial = special.Product.ProductPrice - (special.Product.ProductPrice * 0.10);
        //                break;
        //            case 4:
        //                special.ProductId = 5;
        //                productSpecial = special.Product.ProductPrice - (special.Product.ProductPrice * 0.10);
        //                break;
        //            case 5:
        //                special.ProductId = 6;
        //                productSpecial = special.Product.ProductPrice - (special.Product.ProductPrice * 0.10);
        //                break;
        //            case 6:
        //                special.ProductId = 7;
        //                productSpecial = special.Product.ProductPrice - (special.Product.ProductPrice * 0.10);
        //                break;
        //                default;
        //        }
        //    }
        //}
    }
}
