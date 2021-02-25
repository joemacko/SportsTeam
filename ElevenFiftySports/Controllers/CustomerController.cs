using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using ElevenFiftySports.Data;
using ElevenFiftySports.Models.OrderModels;
using ElevenFiftySports.Services;
using System.Linq;
using System.Web;
using System.Web.Http;
using ElevenFiftySports.Models.CustomerModels;
using System.Threading.Tasks;

namespace ElevenFiftySports.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private CustomerService CreateCustomerSevice()
        {
            var customerId = Guid.Parse(Customer.Identity.GetCustomerId());
            var customerService = new CustomerSevice(customerId);
            return customerService;
        }

        public IHttpActionResult Get
        {
            get
            {
                CustomerService customerService = CreateCustomerSevice();
                var customer = customerService.GetCustomer();
                return Ok(customer);
            }
        }

        public IHttpActionResult Post(CustomerCreate customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateCustomerSevice();
            if (!service.CreateCustomer(customer))
                return InternalServerError();
            return Ok();
        }

        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public async Task<IHttpActionResult> PostCustomerAsync([FromBody] Customer model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empyty!");
            }
            if (ModelState.IsValid)
            {
                _context.Customers.Add(model);
                int changeCount = await _context.SaveChangesAsync();

                return Ok("A new customer has been created in the Database!");
            }
            return BadRequest(ModelState);
        }
    }
}
