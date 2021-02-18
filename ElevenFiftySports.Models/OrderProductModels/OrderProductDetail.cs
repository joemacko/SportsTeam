using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models.OrderProductModels
{
    public class OrderProductDetail
    {
        public int PrimaryId { get; set; }
        public int OrderId { get; set; }
        public string CustomerFirstName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public decimal CostOfOrderProduct { get; set;} //In service will need to set to x.Product.ProductPrice * decimal.Parse(x.ProductCount)  ...or something like that.
    }
}
