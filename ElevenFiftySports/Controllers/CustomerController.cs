using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using ElevenFiftySports.Data;
using ElevenFiftySports.Models.OrderModels;
using ElevenFiftySports.Services;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using ElevenFiftySports.Models.CustomerModels;

namespace ElevenFiftySports.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private CustomerService CreateCustomerService()

        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var customerService = new CustomerService(userId);
            return customerService;
        }


        public IHttpActionResult Get()
        {
            CustomerService customerService = CreateCustomerService();
            var customers = customerService.GetCustomers();
            return Ok(customers);
        }

        public IHttpActionResult Post(CustomerCreate customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateCustomerService();
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
            var service = CreateCustomerService();
            if (!service.UpdateCustomer(customer))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateCustomerService();
            if (!service.DeleteCustomer(id))
                return InternalServerError();
            return Ok();
        }

    }
}
