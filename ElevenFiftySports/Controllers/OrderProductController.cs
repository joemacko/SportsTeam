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

            //using (var ctx = new ApplicationDbContext())
            //{
            //Product product = ctx.Products.Find(model.ProductId);

            //if (model.ProductCount >= product.UnitCount)
            //return BadRequest($"There is not enough inventory of {product.ProductName} available to create this OrderProduct. The current inventory is {product.UnitCount}.");

            //if (!service.CreateOrderProduct(model))
            //return InternalServerError();

            //product.UnitCount -= model.ProductCount;

            //ctx.SaveChanges();

            //return Ok("The OrderProduct has been created.");
            //}

            string created = service.CreateOrderProduct(model);

            if(created.Contains("There is not enough inventory"))
            return BadRequest(created);

            if (created.Contains("finalized"))
                return BadRequest(created);

            if (created.Contains("The OrderProduct"))
            return Ok(created); //stretch goal to keep service as a bool.

            return InternalServerError();

        }

        public IHttpActionResult Put([FromUri] int id, OrderProductEdit updatedOrderProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderProductService();

            //using (var ctx = new ApplicationDbContext())
            //{
            //OrderProduct originalOrderProduct = ctx.OrderProducts.Find(id);
            //Product originalProduct = ctx.Products.Find(originalOrderProduct.ProductId);
            //Product updatedProduct = ctx.Products.Find(updatedOrderProduct.ProductId);

            //if (originalOrderProduct.ProductId == updatedOrderProduct.ProductId)
            //{
            //    if (updatedOrderProduct.ProductCount >= originalProduct.UnitCount + originalOrderProduct.ProductCount) //check if enough original product is in inventory (current inventory + whatever was in the original order)
            //        return BadRequest($"There is not enough inventory of {originalProduct.ProductName} available to update to this OrderProduct's ProductCount. The current inventory (including the ProductCount on the original OrderProduct) is {originalProduct.UnitCount + originalOrderProduct.ProductCount}.");

            //    if (!service.UpdateOrderProduct(id, updatedOrderProduct))
            //        return InternalServerError();

            //    originalProduct.UnitCount += originalOrderProduct.ProductCount; //return original request to inventory
            //    originalProduct.UnitCount -= updatedOrderProduct.ProductCount; //remove current request from inventory

            //    ctx.SaveChanges();

            //    return Ok($"The OrderProduct ID: {id} has been updated.");
            //}

            //else //if original orderproduct product is different from updated orderproduct product
            //{
            //    if (updatedOrderProduct.ProductCount >= updatedProduct.UnitCount)
            //        return BadRequest($"There is not enough inventory of {updatedProduct.ProductName} available to update this OrderProduct's Product and ProductCount. The current inventory is {updatedProduct.UnitCount}."); //check if enough updated product is in inventory

            //    if (!service.UpdateOrderProduct(id, updatedOrderProduct))
            //        return InternalServerError();

            //    originalProduct.UnitCount += originalOrderProduct.ProductCount; //return the original orderproduct to inventory (like you didn't mean to add to order)
            //    updatedProduct.UnitCount -= updatedOrderProduct.ProductCount; //Subtract new orderproduct request from new product's inventory

            //    ctx.SaveChanges();

            //    return Ok($"The OrderProduct ID: {id} has been updated.");
            //}
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

            //using (var ctx = new ApplicationDbContext())
            //{
            //    OrderProduct orderProduct = ctx.OrderProducts.Find(id);
            //    Product product = ctx.Products.Find(orderProduct.ProductId);

            //    if (!service.DeleteOrderProduct(id))
            //        return InternalServerError();

            //    product.UnitCount += orderProduct.ProductCount; //return orderProduct to product inventory

            //    ctx.SaveChanges();

            //    return Ok($"OrderProduct ID: {id} was successfully deleted.");
            //}

            if (!service.DeleteOrderProduct(id))
                return InternalServerError();

            return Ok($"OrderProduct ID: {id} was successfully deleted.");

        }
    }
}