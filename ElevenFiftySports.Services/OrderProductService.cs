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
            var entity =
                new OrderProduct()
                {
                    OrderId = model.OrderId,
                    ProductId = model.ProductId,
                    ProductCount = model.ProductCount
                };

            using (var ctx = new ApplicationDbContext())
            {
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
                    //.Where(e => e.CustomerId == _userId)
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
                            //IndividualProductPrice = e.Product.ProductPrice
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

        public bool UpdateOrderProduct(int id, OrderProductEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .OrderProducts
                    .Single(e => e.PrimaryId == id);

                entity.OrderId = model.OrderId;
                entity.ProductId = model.ProductId;
                entity.ProductCount = model.ProductCount;

                return ctx.SaveChanges() == 1;
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
