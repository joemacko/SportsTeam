using ElevenFiftySports.Data;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.OrderDetailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Services
{
    public class OrderDetailService
    {
        private readonly Guid _userId;

        public OrderDetailService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateOrderDetail(OrderDetailCreate model)
        {
            var entity =
                new OrderDetail()
                {
                    OrderId = model.OrderId,
                    ProductId = model.ProductId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.OrderDetails.Add(entity);
                return ctx.SaveChanges() == 1; 
            }
        }

        //too lazy to make an orderdetailread (it shows the same stuff)
        public IEnumerable<OrderDetailCreate> GetOrderDetails() 
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .OrderDetails
                    //.Where(e => e.CustomerId == _userId)
                    .Select(
                        e =>
                        new OrderDetailCreate
                        {
                            OrderId = e.OrderId,
                            ProductId = e.ProductId                        
                        }
                        );
                return query.ToList();
            }
        }
    }
}
