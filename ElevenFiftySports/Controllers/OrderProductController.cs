using ElevenFiftySports.Models.OrderProductModels;
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
    public class OrderProductController : ApiController
    {
        private OrderProductService CreateOrderDetailService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var orderDetailService = new OrderProductService(userId);
            return orderDetailService;
        }

        public IHttpActionResult Get()
        {
            OrderProductService orderDetailService = CreateOrderDetailService();
            var orderDetails = orderDetailService.GetOrderProducts();
            return Ok(orderDetails);
        }

        public IHttpActionResult Post(OrderProductCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderDetailService();

            if (!service.CreateOrderProduct(model))
                return InternalServerError();

            return Ok();
        }
    }
}