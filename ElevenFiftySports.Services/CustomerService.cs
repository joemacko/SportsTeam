using ElevenFiftySports.Data;
using ElevenFiftySports.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Services
{
    public class CustomerService
    {
        private readonly Guid _userId;
        private Guid CustomerId;
        private object model;

        public CustomerService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateCustomer(CustomerCreate model)
        {
            var entity =
                new Customer()
                {
                    CustomerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email, //--CJ 3.3.21
                    CellPhoneNumber = model.CellPhoneNumber, //--CJ 3.3.21
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }
        public IEnumerable<CustomerList> GetCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Customers
                        .Where(e => e.CustomerId == _userId)
                        .Select(
                            e =>
                                new CustomerList
                                {
                                    CustomerId = e.CustomerId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    CreatedUtc = e.CreatedUtc
                                }
                                );
                return query.ToArray();

            }
        }
        //public CustomerDetail GetCustomerById(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Customers
        //                .Single(e => e.CustomerId == model.CustomerId);
        //        return
        //            new CustomerDetail
        //            {
        //                CustomerId = entity.CustomerId,
        //                FirstName = entity.FirstName,
        //                LastName = entity.LastName,
        //                CreatedUtc = entity.CreateUtc,
        //                ModifiedUtc = entity.ModifiedUtc
        //            };
        //    }
        //}
        public bool UpdateCustomer(CustomerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerId == CustomerId);

                entity.CustomerId = model.CustomerId;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email;
                entity.CellPhoneNumber = model.CellPhoneNumber;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCustomer(int customerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Customers
                    .Single(e => e.CustomerId == CustomerId);
                ctx.Customers.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
