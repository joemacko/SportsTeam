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
    //[Authorize]
    //public class CustomerController : ApiController
    //{
    //    private CustomerService CreateCustomerSevice()
    //    {
    //        var customerId = Guid.Parse(Customer.Identity.GetCustomerId());
    //        var customerService = new CustomerSevice(customerId);
    //        return customerService;
    //    }

    //    public IHttpActionResult Get
    //    {
    //        get
    //        {
    //            CustomerService customerService = CreateCustomerSevice();
    //            var customer = customerService.GetCustomer();
    //            return Ok(customer);
    //        }
    //    }

    //    public IHttpActionResult Post(CustomerCreate customer)
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);
    //        var service = CreateCustomerSevice();
    //        if (!service.CreateCustomer(customer))
    //            return InternalServerError();
    //        return Ok();

    //        public IHttpActionResult Get()
    //    {
    //        CustomerService customerService = CreateCustomerService();
    //        var customers = customerService.GetCustomers();
    //        return Ok(customers);
    //    }

    //    public IHttpActionResult Post(CustomerCreate customer)
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);
    //        var service = CreateCustomerService();
    //        if (!service.CreateCustomer(customer))
    //            return InternalServerError();
    //        return Ok();
    //    }
        
    //    private CustomerService CreateCustomerService()
    //    {
    //        var userId = Guid.Parse(User.Identity.GetUserId());
    //        var customerService = new CustomerService(userId);
    //        return customerService;
    //    }
    //        public IHttpActionResult GetCustomerById(int id)
    //        {
    //            using (var ctx = new ApplicationDbContext())
    //            {
    //                var entity =
    //                    ctx
    //                        .Customers
    //                        .Single(e => e.CustomerId == id && e.OwnerId == _userId);
    //                return
    //                    new CustomerDetail
    //                    {
    //                        CustomerId = entity.CustomerId,
    //                        FirstName = entity.FirstName,
    //                        LastName = entity.LastName,
    //                        Email = entity.Email,
    //                        CellPhoneNumber = entity.CellPhoneNumber,
    //                        CreatedUtc = entity.CreatedUtc,
    //                        ModifiedUtc = entity.ModifiedUtc
    //                    };
    //            }
    //        }

    //        public IHttpActionResult Put(CustomerEdit customer)
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);
    //        var service = CreateCustomerService();
    //        if (!service.UpdateCustomer(customer))
    //            return InternalServerError();
    //        return Ok();
    //    }

    //    public IHttpActionResult Delete (int id)
    //    {
    //        var service = CreateCustomerService();
    //        if (!service.DeleteCustomer(id))
    //            return InternalServerError();
    //        return Ok();
    //    }

    //}
}
