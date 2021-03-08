using System;
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

        [Required, ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public DateTimeOffset CreatedOrderDate { get; set; } 

        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        [NotMapped]
        public double TotalCost  
        {
            get
            {
                double cost = 0;

                foreach (OrderProduct op in OrderProducts)
                {
                    if (!op.Product.IsSpecial)
                        cost += (op.Product.ProductPrice * op.ProductCount);

                    else
                    {
                        int countOfSpecialRuns = 0; //used to track if there were any changes within the special foreach BECAUSE I only want it to add the normal product cost one time (not foreach).

                        foreach (Special s in op.Product.ProductSpecials)
                        {
                            if (CreatedOrderDate.DayOfWeek == s.DayOfWeek)
                            {
                                cost += (s.ProductSpecialPrice * op.ProductCount);
                                countOfSpecialRuns++;
                            }
                        }

                        if (countOfSpecialRuns == 0) //this means that none of the specials were on the day of the order (for the product)
                            cost += (op.Product.ProductPrice * op.ProductCount); //run like a non-special product
                    }
                }
                return OrderProducts.Count > 0 ? cost : 0;
            }
        }

        public double FinalTotalCost { get; set; }
        
        public bool OrderFinalized {get; set;} 
       
    }
}
