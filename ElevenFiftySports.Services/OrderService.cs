using ElevenFiftySports.Data;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.OrderProductModels;
using ElevenFiftySports.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElevenFiftySports.Services
{
    public class OrderService
    {
        private readonly Guid _userId;

        public OrderService(Guid userId)
        {
            _userId = userId;
        }

        public string CreateOrder()//no input needed, all info is given already to create the foundation of an order
        {
            var entity =
                new Order()
                {
                    CustomerId = _userId,
                    CreatedOrderDate = DateTime.Now,
                    OrderFinalized = false,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Orders.Add(entity);

                ctx.SaveChanges();

                return $"The Order ID: {entity.OrderId} has been created."; //when bool, ctx.savechanges == 1
            }
        }

        public IEnumerable<OrderListItem> GetOrders() //For customer that is logged in
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Orders
                    .Where(e => e.CustomerId == _userId).ToList();

                List<OrderListItem> newList = new List<OrderListItem>();

                foreach (var q in query)
                {
                    var oLI = new OrderListItem
                    {
                        OrderId = q.OrderId,
                        CustomerId = q.CustomerId,
                        CustomerFirstName = q.Customer.FirstName,
                        OrderProducts = HelperConvertOrderProductsToOPListItem(q.OrderProducts),
                        Cost = q.OrderFinalized ? q.FinalTotalCost : q.TotalCost,
                        CreatedOrderDate = q.CreatedOrderDate,
                        OrderFinalized = q.OrderFinalized
                    };
                    newList.Add(oLI);
                }

                return newList;
            }
        }

        public OrderListItem GetMostRecentOrder() //For customer that is logged in
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Orders
                    .Where(e => e.CustomerId == _userId)
                    .OrderByDescending(e => e.CreatedOrderDate)
                    .First();

                return
                    new OrderListItem
                    {
                        OrderId = entity.OrderId,
                        CustomerId = entity.CustomerId,
                        CustomerFirstName = entity.Customer.FirstName,
                        OrderProducts = HelperConvertOrderProductsToOPListItem(entity.OrderProducts),
                        Cost = entity.OrderFinalized ? entity.FinalTotalCost : entity.TotalCost,
                        CreatedOrderDate = entity.CreatedOrderDate,
                        OrderFinalized = entity.OrderFinalized
                    };
            }

        }

        public OrderDetail GetOrderById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                ctx
                .Orders
                .Single(e => e.OrderId == id);

                return
                    new OrderDetail
                    {
                        OrderId = entity.OrderId,
                        CreatedOrderDate = entity.CreatedOrderDate,
                        CustomerId = entity.CustomerId,
                        CustomerFirstName = entity.Customer.FirstName,
                        OrderProducts = HelperConvertOrderProductsToOPListItem(entity.OrderProducts),
                        Cost = entity.OrderFinalized ? entity.FinalTotalCost : entity.TotalCost,
                        OrderFinalized = entity.OrderFinalized
                    };

            }
        }

        public bool UpdateOrderToFinalize(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Orders
                    .Single(e => e.OrderId == id);

                entity.FinalTotalCost = entity.TotalCost;
                entity.OrderFinalized = true;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteOrder(int orderID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var order =
                    ctx
                    .Orders
                    .Single(e => e.OrderId == orderID);


                if (order.OrderProducts.Count > 0)
                {
                    foreach (OrderProduct orderProduct in order.OrderProducts.ToList())
                    {
                        var product =
                            ctx
                            .Products
                            .Single(p => p.ProductId == orderProduct.ProductId);

                        product.UnitCount += orderProduct.ProductCount; //return orderProduct to product inventory

                        ctx.OrderProducts.Remove(orderProduct);
                    }
                }

                ctx.Orders.Remove(order);

                return ctx.SaveChanges() >= 1;//don't know how many changes there are because it is as many as there are orderproducts.
            }
        }

        public List<OrderProductListItem> HelperConvertOrderProductsToOPListItem(List<OrderProduct> orderProducts) //Necessary because postman cannot return classes (OrderProduct) as a datatype
        {
            List<OrderProductListItem> newList = new List<OrderProductListItem>();
            foreach (var op in orderProducts)
            {
                var listItem = new OrderProductListItem
                {
                    PrimaryId = op.PrimaryId,
                    CustomerFirstName = op.Order.Customer.FirstName,
                    ProductId = op.ProductId,
                    ProductCount = op.ProductCount,
                    OrderId = op.OrderId,
                    ProductName = op.Product.ProductName,
                };
                newList.Add(listItem);
            }
            return newList;
        }
    }
}
