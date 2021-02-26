using ElevenFiftySports.Data;
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
        private readonly Guid customerId;       
            public CustomerService(Guid customerId, Guid _customerId)
        {
            _customerId = customerId;
        }
        public bool CreateCustomer()
        {
            var entity = new Customer()
            {
                CustomerId = customerId,
                CreatedUtc = DateTimeOffset.Now
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
       
        }
       
        
    }
}
