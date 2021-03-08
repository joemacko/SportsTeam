using ElevenFiftySports.Data;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElevenFiftySports.Services
{
    public class OrderProductService
    {
        private readonly Guid _userId;

        public OrderProductService(Guid userId)
        {
            _userId = userId;
        }

        public string CreateOrderProduct(OrderProductCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var order =
                    ctx
                    .Orders
                    .Single(o => o.OrderId == model.OrderId);

                if(order.OrderFinalized)
                {
                    return "This order has been finalized. You cannot make updates to it.";
                }

                var entity =
                new OrderProduct()
                {
                    OrderId = model.OrderId,
                    ProductId = model.ProductId,
                    ProductCount = model.ProductCount
                };


                var product =
                    ctx
                    .Products
                    .Single(p => p.ProductId == model.ProductId);

                if (model.ProductCount >= product.UnitCount)
                    return $"There is not enough inventory of {product.ProductName} available to create this OrderProduct. The current inventory is {product.UnitCount}.";

                product.UnitCount -= model.ProductCount;

                ctx.OrderProducts.Add(entity);
                ctx.SaveChanges();

                return $"The OrderProduct {entity.PrimaryId} has been created."; //when bool, savechanges == 2
            }
        }

        public IEnumerable<OrderProductListItem> GetOrderProducts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .OrderProducts
                    .Select(
                        e =>
                        new OrderProductListItem
                        {
                            PrimaryId = e.PrimaryId,
                            OrderId = e.OrderId,
                            CustomerFirstName = e.Order.Customer.FirstName,
                            ProductId = e.ProductId,
                            ProductName = e.Product.ProductName,
                            ProductCount = e.ProductCount,
                        }
                        );
                return query.ToList();
            }
        }

        public OrderProductDetail GetOrderProductById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .OrderProducts
                    .Single(e => e.PrimaryId == id);
                return
                new OrderProductDetail
                {
                    PrimaryId = entity.PrimaryId,
                    OrderId = entity.OrderId,
                    CustomerFirstName = entity.Order.Customer.FirstName,
                    ProductId = entity.ProductId,
                    ProductCount = entity.ProductCount,
                    ProductName = entity.Product.ProductName
                };
            }
        }

        public string UpdateOrderProduct(int id, OrderProductEdit updatedOrderProduct)
        {
            using (var ctx = new ApplicationDbContext())
            {

                OrderProduct originalOrderProduct = ctx.OrderProducts.Find(id);

                if (originalOrderProduct.Order.OrderFinalized)
                {
                    return "This order has been finalized. You cannot make updates to it.";
                }

                Product originalProduct =
                    ctx
                    .Products
                    .Single(oP => oP.ProductId == originalOrderProduct.ProductId);

                Product updatedProduct =
                    ctx
                    .Products
                    .Single(uP => uP.ProductId == updatedOrderProduct.ProductId);

                if (originalOrderProduct.ProductId == updatedOrderProduct.ProductId)
                {
                    if (updatedOrderProduct.ProductCount >= originalProduct.UnitCount + originalOrderProduct.ProductCount) //check if enough original product is in inventory (current inventory + whatever was in the original order)
                        return $"There is not enough inventory of {originalProduct.ProductName} available to update to this OrderProduct's ProductCount. The current inventory (including the ProductCount on the original OrderProduct) is {originalProduct.UnitCount + originalOrderProduct.ProductCount} units.";

                    originalProduct.UnitCount += originalOrderProduct.ProductCount; //return original request to inventory
                    originalProduct.UnitCount -= updatedOrderProduct.ProductCount; //remove current request from inventory

                    originalOrderProduct.OrderId = updatedOrderProduct.OrderId;
                    originalOrderProduct.ProductId = updatedOrderProduct.ProductId;
                    originalOrderProduct.ProductCount = updatedOrderProduct.ProductCount;

                    ctx.SaveChanges();

                    return $"The OrderProduct ID: {id} has been updated.";
                }

                else //if original orderproduct product is different from updated orderproduct product
                {
                    if (updatedOrderProduct.ProductCount >= updatedProduct.UnitCount)
                        return $"There is not enough inventory of the updated product: {updatedProduct.ProductName} available to update this OrderProduct's Product and ProductCount. The current {updatedProduct.ProductName} inventory is {updatedProduct.UnitCount} units."; //check if enough updated product is in inventory

                    originalProduct.UnitCount += originalOrderProduct.ProductCount; //return the original orderproduct to inventory (like you didn't mean to add to order)
                    updatedProduct.UnitCount -= updatedOrderProduct.ProductCount; //Subtract new orderproduct request from new product's inventory

                    originalOrderProduct.OrderId = updatedOrderProduct.OrderId;
                    originalOrderProduct.ProductId = updatedOrderProduct.ProductId;
                    originalOrderProduct.ProductCount = updatedOrderProduct.ProductCount;

                    ctx.SaveChanges();

                    return $"The OrderProduct ID: {id} has been updated with the updated product: {updatedProduct.ProductName}.";
                }
            }
        }

        public bool DeleteOrderProduct(int oPID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .OrderProducts
                    .Single(e => e.PrimaryId == oPID);

                var product =
                    ctx
                    .Products
                    .Single(p => p.ProductId == entity.ProductId);

                product.UnitCount += entity.ProductCount; //return orderProduct to product inventory

                ctx.OrderProducts.Remove(entity);

                return ctx.SaveChanges() == 2;
            }
        }
    }
}
