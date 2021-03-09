using ElevenFiftySports.Data;
using ElevenFiftySports.Models;
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

        public IHttpActionResult Get([FromUri] int id)
        {
            OrderProductService oPS = CreateOrderProductService();
            var orderProduct = oPS.GetOrderProductById(id);
            return Ok(orderProduct);
        }

        public IHttpActionResult Post(OrderProductCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderProductService();

            string created = service.CreateOrderProduct(model);

            if(created.Contains("There is not enough inventory"))
            return BadRequest(created);

            if (created.Contains("finalized"))
                return BadRequest(created);

            if (created.Contains("The OrderProduct"))
            return Ok(created);

            return InternalServerError();

        }

        public IHttpActionResult Put([FromUri] int id, OrderProductEdit updatedOrderProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderProductService();

            string updated = service.UpdateOrderProduct(id, updatedOrderProduct);

            if (updated.Contains("There is not enough inventory"))
                return BadRequest(updated);

            if (updated.Contains("finalized"))
                return BadRequest(updated);
            
            if (updated.Contains("The OrderProduct"))
                return Ok(updated);

            return InternalServerError();
        }

        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateOrderProductService();

            if (!service.DeleteOrderProduct(id))
                return InternalServerError();

            return Ok($"OrderProduct ID: {id} was successfully deleted.");

        }
    }
}