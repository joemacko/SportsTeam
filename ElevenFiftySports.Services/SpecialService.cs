using ElevenFiftySports.Data;
using ElevenFiftySports.Models.SpecialModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Services
{
    public class SpecialService
    {
        private readonly int _productId;

        // Need to actually implement a GetProductById method
        // Should I change my "SpecialReadByDay" class to "SpecialReadById class and have it apply to both?
        //public IEnumerable<SpecialReadByDay> GetSpecialByProductId(int productId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .Specials
        //                .Where(e => e.ProductId == _productId)
        //                .Select(
        //                    e =>
        //                        new SpecialReadByDay
        //                        {
        //                            SpecialId = e.SpecialId,
        //                            DayOfWeek = e.DayOfWeek,
        //                            ProductSpecialPrice = e.ProductSpecialPrice
        //                        }
        //                );

        //        return query.ToArray();
        //    }
        //}

        public SpecialService() { }

        public SpecialService(int productId)
        {
            _productId = productId;
        }

        public bool CreateSpecial(SpecialCreate model)
        {
            var entity =
                new Special()
                {
                    ProductId = model.ProductId,
                    SpecialId = model.SpecialId,
                    DayOfWeek = model.DayOfWeek,
                    ProductSpecialPrice = model.ProductSpecialPrice
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Specials.Add(entity);
                return ctx.SaveChanges() >= 1;
            }
        }

        public IEnumerable<SpecialRead> GetSpecials()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Specials
                        .Where(e => e.ProductId == _productId)
                        .Select(
                            e =>
                                new SpecialRead
                                {
                                    SpecialId = e.SpecialId,
                                    DayOfWeek = e.DayOfWeek,
                                    ProductSpecialPrice = e.ProductSpecialPrice
                                }
                        );

                return query.ToArray();
            }
        }

        public IEnumerable<SpecialReadByDay> GetSpecialByDay(DayOfWeek dayOfWeek)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Specials
                        .Where(e => e.DayOfWeek == dayOfWeek)
                        .Select(
                            e =>
                                new SpecialReadByDay
                                {
                                    SpecialId = e.SpecialId,
                                    DayOfWeek = e.DayOfWeek,
                                    ProductSpecialPrice = e.ProductSpecialPrice
                                }
                        );

                return query.ToArray();
            }
        }

        public bool UpdateSpecial(SpecialUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Specials
                        .Single(e => e.SpecialId == model.SpecialId);

                entity.ProductId = model.ProductId;
                entity.DayOfWeek = model.DayOfWeek;
                entity.ProductSpecialPrice = model.ProductSpecialPrice;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSpecial(int specialId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Specials
                        .Single(e => e.SpecialId == specialId);

                ctx.Specials.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
