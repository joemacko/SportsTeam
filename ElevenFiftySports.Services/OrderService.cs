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

        public bool CreateOrder()//no input needed, all info is given already
        {
            var entity =
                new Order()
                {
                    CustomerId = _userId,
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
                            CustomerId = e.CustomerId,
                            //OrderDetails = e.OrderDetails.ToList() //added .ToList for possible formatting here and line 55(?)
                            //ProductIds = GetProductIdsFromOrderDetails(e.OrderDetails.ToList()) //Is it possible to send list of lists to JSON (read in postman? getting system.notsupportedexception: LINQ to entities does not recognize the method Int32 for....)
                            //TotalCost = e.TotalCost,
                        }
                        );
                return query.ToList();
            }
        }

        public IEnumerable<int> GetProductIdsFromOrderDetails(List<OrderDetail> model)
        {
            List<int> newList = new List<int>();

            foreach (OrderDetail od in model)
            {
                newList.Add(od.ProductId);
            }

            return newList;
        }
    }
}
