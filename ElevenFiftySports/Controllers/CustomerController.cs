using System;
using Microsoft.AspNet.Identity;
using ElevenFiftySports.Data;
using ElevenFiftySports.Services;
using System.Web.Http;
using ElevenFiftySports.Models.CustomerModels;
using System.Threading.Tasks;

namespace ElevenFiftySports.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {

        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public async Task<IHttpActionResult> PostCustomerAsync([FromBody] Customer model)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var customerService = new CustomerService(userId);
            return customerService;
        }


        public IHttpActionResult Get()
        {
            CustomerService customerService = CustomerCreateService();
            var customers = customerService.GetCustomers();
            return Ok(customers);
        }

        public IHttpActionResult Post(CustomerCreate customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CustomerCreateService();
            if (!service.CreateCustomer(customer))
                return InternalServerError();
            return Ok();
        }
        

        //public IHttpActionResult Get(int id)
        //{
        //    CustomerService customerService = CreateCustomerService();
        //    var customer = customerService.GetCustomerById(id);
        //    return Ok(customer);
        //}

        public IHttpActionResult Put(CustomerEdit customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CustomerCreateService();
            if (!service.UpdateCustomer(customer))
                return InternalServerError();
            return Ok();
        }


    }
}
