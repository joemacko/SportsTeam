using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElevenFiftySports.Models.CustomerModels;
using ElevenFiftySports.Data;

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


    }
}
