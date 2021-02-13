using ElevenFiftySports.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Models
{
    public class OrderRead
    {
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        //public List<OrderDetail> OrderDetails { get; set; }
        public int[] ProductIds { get; set; }
        //public double TotalCost
        //{
        //    get
        //    {
        //        double cost = 0;

        //        foreach (var od in OrderDetails)
        //        {
        //            cost += od.Product.ProductPrice;
        //        }

        //        return OrderDetails.Count > 0 ? cost : 0;
        //    }
        //}
        //public double TotalCost { get; set; }
    }
}
