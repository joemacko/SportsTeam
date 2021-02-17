using ElevenFiftySports.Data;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.OrderDetailModels;
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
                    .Where(e => e.CustomerId == _userId).ToList()
                    .Select(
                        e =>
                        //{
                            //var example = 
                            new OrderRead
                            {
                                OrderId = e.OrderId,
                                CustomerId = e.CustomerId,
                                //OrderDetails = (List<Models.OrderDetailModels.OrderDetailRead>)e.OrderDetails     //WHERE I LEFT OFF
                                //OrderDetails = GetOrderDetails(e.OrderDetails)
                                //OrderDetails = e.OrderDetails.ToList() //added .ToList for possible formatting here and line 55(?)
                                //ProductIds = GetProductIdsFromOrderDetails(e.OrderDetails.ToList()) //Is it possible to send list of lists to JSON (read in postman? getting system.notsupportedexception: LINQ to entities does not recognize the method Int32 for....) -- RELATED TO HELPER METHOD
                                //TotalCost = e.TotalCost,
                            }
                            //foreach(var x in e.OrderDetails)
                            //{
                            //    example.OrderDetails.Add(new OrderDetailRead { ProductId = x.ProductId });
                            //}
                            ////example.OrderDetails = GetOrderDetails(e.OrderDetails);
                            //return example;
                        //}
                        );
                foreach(OrderRead or in query)
                {
                    var order = ctx.Orders.Find(or.OrderId);
                    foreach(OrderDetail od in order.OrderDetails)
                    {
                        or.OrderDetails.Add(new OrderDetailRead { ProductId = od.ProductId });//ctx.OrderDetails.Where(o => o.OrderId == or.OrderId).Select(o => o.ProductId) });
                        //OrderDetailRead orderDetailRead = new OrderDetailRead();
                        //orderDetailRead.ProductId = od.ProductId;
                        //or.OrderDetails.Add(orderDetailRead);

                    }
                }
                return query.ToList();
            }
        }

        //BELOW WAS PROJECT WITH LA - TO USE HELPER METHOD BECAUSE IT WAS NOT SHOWING UP ABOVE
        //public List<OrderDetailRead> GetOrderDetails(List<OrderDetail> model)
        //{
        //    //List<int> newList = new List<int>();
        //    List<OrderDetailRead> newList = new List<OrderDetailRead>();

        //    foreach (OrderDetail od in model)
        //    {
        //        //newList.Add(od.ProductId);
        //        OrderDetailRead odr = new OrderDetailRead();
        //        odr.ProductId = od.ProductId;
        //        newList.Add(odr);
        //    }

        //    return newList;
        //}
        //public List<OrderDetailRead> GetOrderDetails2(int orderId)
        //{
        //    //List<int> newList = new List<int>();
        //    List<OrderDetailRead> newList = new List<OrderDetailRead>();

        //    var ctx = new ApplicationDbContext();
            
        //    var model = ctx.OrderDetails.Find(orderId); //need to figure out how to make this a list
            
        //    foreach (OrderDetail od in model)
        //    {
        //        //newList.Add(od.ProductId);
        //        OrderDetailRead odr = new OrderDetailRead();
        //        odr.ProductId = od.ProductId;
        //        newList.Add(odr);
        //    }

        //    return newList;
        //}
    }
}
