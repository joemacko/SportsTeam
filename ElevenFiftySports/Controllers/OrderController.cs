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

        public IHttpActionResult Post()
        {
            OrderService orderService = CreateOrderService();

            if (!orderService.CreateOrder())
                        return InternalServerError();
            
            return Ok();
        }
    }
}