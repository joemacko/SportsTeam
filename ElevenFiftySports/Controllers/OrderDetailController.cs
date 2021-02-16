using ElevenFiftySports.Models.OrderDetailModels;
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
    public class OrderDetailController : ApiController
    {
        private OrderDetailService CreateOrderDetailService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var orderDetailService = new OrderDetailService(userId);
            return orderDetailService;
        }

        public IHttpActionResult Get()
        {
            OrderDetailService orderDetailService = CreateOrderDetailService();
            var orderDetails = orderDetailService.GetOrderDetails();
            return Ok(orderDetails);
        }

        public IHttpActionResult Post(OrderDetailCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderDetailService();

            if (!service.CreateOrderDetail(model))
                return InternalServerError();

            return Ok();
        }
    }
}