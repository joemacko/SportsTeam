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
        private OrderProductService CreateOrderProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var orderProductService = new OrderProductService(userId);
            return orderProductService;
        }

        public IHttpActionResult Get()
        {
            OrderProductService orderProductService = CreateOrderProductService();
            var orderProducts = orderProductService.GetOrderProducts();
            return Ok(orderProducts);
        }

        public IHttpActionResult Post(OrderProductCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderProductService();

            if (!service.CreateOrderProduct(model))
                return InternalServerError();

            return Ok();
        }
    }
}