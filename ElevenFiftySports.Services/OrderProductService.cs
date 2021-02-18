using ElevenFiftySports.Data;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.OrderProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Services
{
    public class OrderProductService
    {
        private readonly Guid _userId;

        public OrderProductService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateOrderProduct(OrderProductCreate model)
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
                ctx.OrderProducts.Add(entity);
                return ctx.SaveChanges() == 1; 
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
                            ProductId = e.ProductId,
                            ProductCount = e.ProductCount
                        }
                        );
                return query.ToList();
            }
        }
    }
}
