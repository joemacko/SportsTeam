using ElevenFiftySports.Data;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.OrderProductModels;
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

        public bool CreateOrder()//no input needed, all info is given already to create the foundation of an order
        {
            var entity =
                new Order()
                {
                    CustomerId = _userId,
                    CreatedOrderDate = DateTime.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Orders.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<OrderListItem> GetOrders()
        {
           
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Orders
                    .Where(e => e.CustomerId == _userId).ToList();

                List<OrderListItem> newList = new List<OrderListItem>();

               foreach(var q in query)
                {
                    var oLI = new OrderListItem
                    {
                        OrderId = q.OrderId,
                        CustomerId = q.CustomerId,
                        CustomerFirstName = q.Customer.FirstName,
                        OrderProducts = HelperConvertOrderProductsToOPListItem(q.OrderProducts),
                        TotalCost = q.TotalCost
                    };
                    newList.Add(oLI);
                }

                return newList;

                //issue before was that we were returning the **query** and only creating part of an orderlistitem (in prev version). Therefore, the return method was not grabbing what the foreach below was doing. now we have created a new list that utilizes data from the query to return.
            }
        }

        public List<OrderProductListItem> HelperConvertOrderProductsToOPListItem (List<OrderProduct> orderProducts) //Necessary because postman cannot return classes (OrderProduct) as a datatype
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
                    IndividualProductPrice = op.Product.ProductPrice
                };
                newList.Add(listItem);
            }
            return newList;
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




        //                using (var ctx = new ApplicationDbContext())
        //        {
        //            List<OrderProduct> orderProductsEntities;
        //var query =
        //    ctx
        //    .Orders
        //    .Where(e => e.CustomerId == _userId).ToList()
        //    .Select(
        //        e =>
        //            //{
        //            //var example = 
        //            new OrderListItem
        //            {
        //                OrderId = e.OrderId,
        //                CustomerId = e.CustomerId,
        //                CustomerFirstName = e.Customer.FirstName,

        //                            //OrderDetails = (List<Models.OrderDetailModels.OrderDetailRead>)e.OrderDetails     //WHERE I LEFT OFF
        //                            //OrderDetails = GetOrderDetails(e.OrderDetails)
        //                            //OrderDetails = e.OrderDetails.ToList() //added .ToList for possible formatting here and line 55(?)
        //                            //ProductIds = GetProductIdsFromOrderDetails(e.OrderDetails.ToList()) //Is it possible to send list of lists to JSON (read in postman? getting system.notsupportedexception: LINQ to entities does not recognize the method Int32 for....) -- RELATED TO HELPER METHOD
        //                            //TotalCost = e.TotalCost,
        //                        }
        //        //foreach(var x in e.OrderDetails)
        //        //{
        //        //    example.OrderDetails.Add(new OrderDetailRead { ProductId = x.ProductId });
        //        //}
        //        ////example.OrderDetails = GetOrderDetails(e.OrderDetails);
        //        //return example;
        //        //}
        //        );
        ////var query = request;
        ///
        //foreach (OrderListItem orderListItem in query) //??? - Need to write as a method that takes in a List<OrderListItem>??? Issue is that all of this lives within using context instead of ctx field at top...
        //            {
        //                var order = ctx.Orders.Find(orderListItem.OrderId);
        //                foreach (OrderProduct orderProduct in order.OrderProducts)
        //                {
        //                    orderListItem.OrderProducts.Add(new OrderProductListItem
        //                    {
        //                        PrimaryId = orderProduct.PrimaryId,
        //                        ProductId = orderProduct.ProductId,
        //                        ProductName = orderProduct.Product.ProductName,
        //                        OrderId = orderProduct.OrderId,
        //                        CustomerFirstName = orderProduct.Order.Customer.FirstName,
        //                        ProductCount = orderProduct.ProductCount
        //});

        //                }
        //            }
        //// return query.ToList();

    }
}
