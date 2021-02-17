﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        //public virtual List<Product> OrderProducts{ get; set; } //we can create orderdetails instead
        public virtual List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>(); 
        // public List<Product> Products { get; set; } //remove to match reference https://dev.to/_patrickgod/many-to-many-relationship-with-entity-framework-core-4059  
        [NotMapped]
        public double TotalCost
        {
            get
            {
                double cost = 0;

                foreach (var od in OrderDetails)
                {
                    cost += od.Product.ProductPrice;
                }

                return OrderDetails.Count > 0 ? cost : 0;
            }
        }
    }
}
