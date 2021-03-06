using System;
using Microsoft.AspNet.Identity;
using ElevenFiftySports.Data;
using ElevenFiftySports.Services;
using System.Web.Http;
using ElevenFiftySports.Models.CustomerModels;
using System.Threading.Tasks;
using System.Linq;

namespace ElevenFiftySports.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private CustomerService CreateCustomerSevice()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var customerService = new CustomerService(userId);
            return customerService;
        }

        [HttpPost]
        public IHttpActionResult Post(CustomerCreate customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateCustomerSevice();
            if (!service.CreateCustomer(customer))
                return InternalServerError();
            return Ok();
        }


        [HttpGet]
        [Route("Api/GetAll")]
        public IHttpActionResult Get()
        {
            var customerService = CreateCustomerSevice();
                var customer = customerService.GetCustomers();
                return Ok(customer);
        }

        public IHttpActionResult GetCustomerById([FromUri] Guid customerId)
        {
            CustomerService customerService = CreateCustomerSevice();
            var customer = customerService.GetCustomerById(customerId);
            return Ok(customer);
        }


        public IHttpActionResult Delete([FromUri]Guid customerId)
        {
            var service = CreateCustomerSevice();
            if (!service.DeleteCustomer(customerId))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Put(CustomerEdit customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateCustomerSevice();
            if (!service.UpdateCustomer(customer))
                return InternalServerError();
            return Ok();
        }
        public Guid GetGuidHelper()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return userId;
        }
    }
}
