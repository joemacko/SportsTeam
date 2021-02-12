using ElevenFiftySports.Data;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Services
{
    public class OrderService
    {
        private readonly Guid _userId;

        public OrderService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateOrder(OrderCreate model)
        {
            double tcost = 0;
            foreach (var od in model.OrderDetails)
            {
                tcost += od.Product.ProductPrice; //foreach orderdetail in orderproductlist, add each product's price to the running totalcost (tcost)
            }
            var entity =
                new Order()
                {
                    CustomerId = _userId,
                    OrderDetails = model.OrderDetails,
                    TotalCost = tcost,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Orders.Add(entity);
                return ctx.SaveChanges() == 1; //not sure if this will be multiple changes yet?
            }
        }

        public IEnumerable<OrderRead> GetOrders()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Orders
                    .Where(e => e.CustomerId == _userId)
                    .Select(
                        e =>
                        new OrderRead
                        {
                            OrderId = e.OrderId,
                            OrderDetails = e.OrderDetails.ToList(), //added .ToList for possible formatting(?)
                            TotalCost = e.TotalCost
                        }
                        );
                return query.ToList();
            }
        }
    }
}
