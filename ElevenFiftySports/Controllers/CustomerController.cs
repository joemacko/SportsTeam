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

namespace ElevenFiftySports.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {

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
