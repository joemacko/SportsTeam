using ElevenFiftySports.Data;
using ElevenFiftySports.Models.OrderModels;
using ElevenFiftySports.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ElevenFiftySports.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        private OrderService CreateOrderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var orderService = new OrderService(userId);
            return orderService;
        }

        public IHttpActionResult Get()
        {
            OrderService orderService = CreateOrderService();
            var orders = orderService.GetOrders();
            return Ok(orders);
        }

        [Route("api/Order/MostRecent")]
        public IHttpActionResult GetMostRecentOrder()
        {
            OrderService orderService = CreateOrderService();
            var order = orderService.GetMostRecentOrder();
            return Ok(order);
        }

        public IHttpActionResult GetOrderById([FromUri] int id)
        {
            OrderService orderService = CreateOrderService();
            var order = orderService.GetOrderById(id);
            return Ok(order);
        }

        public IHttpActionResult Post()
        {
            OrderService orderService = CreateOrderService();

            string created = orderService.CreateOrder();

            if (created.Contains("The Order"))
                return Ok(created); 

            return InternalServerError();
        }

        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateOrderService();
            //var opc = new OrderProductController();


            //using (var ctx = new ApplicationDbContext())
            //{
            //    Order order = ctx.Orders.Find(id); //could make this an if ! and return badrequest order id does not exist...

            //    if (order.OrderProducts.Count > 0)
            //    {
            //        foreach (OrderProduct orderProduct in order.OrderProducts)
            //            opc.Delete(orderProduct.PrimaryId);
            //    }

            //    if (!service.DeleteOrder(id))
            //        return InternalServerError();

            //    ctx.SaveChanges();

            //    return Ok($"Order ID: {id} and any associated orderProducts have been deleted.");
            //}

            Guid guid = Guid.Parse(User.Identity.GetUserId());

            if (!service.DeleteOrder(guid, id))
                return InternalServerError();

            return Ok($"Order ID: {id} and any associated orderProducts have been deleted.");

        }

        //below not needed with simple order create model...
        //public IHttpActionResult Post(OrderCreate order)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var service = CreateOrderService();

        //    if (!service.CreateOrder(order))
        //        return InternalServerError();

        //    return Ok();
        //}
    }
}