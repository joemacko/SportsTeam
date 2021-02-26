using ElevenFiftySports.Models.SpecialModels;
using ElevenFiftySports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenFiftySports.Controllers
{
    public class SpecialController : ApiController
    {
        private SpecialService CreateSpecialService()
        {
            // Get product by id
            var specialService = new SpecialService();
            return specialService;
        }

        public IHttpActionResult Post(SpecialCreate special)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSpecialService();

            if (!service.CreateSpecial(special))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get()
        {
            var specialService = CreateSpecialService();
            var specials = specialService.GetSpecials();
            return Ok(specials);
        }

        public IHttpActionResult GetByDay(DayOfWeek dayOfWeek)
        {
            var specialService = CreateSpecialService();
            var special = specialService.GetSpecialByDay(dayOfWeek);
            return Ok(special);
        }

        //public IHttpActionResult GetByProductId(int productId)
        //{
        //    var specialService = CreateSpecialService();
        //    var special = specialService.GetSpecialByProductId(productId);
        //    return Ok(special);
        //}

        public IHttpActionResult Put(SpecialUpdate special)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSpecialService();

            if (!service.UpdateSpecial(special))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int specialId)
        {
            var service = CreateSpecialService();

            if (!service.DeleteSpecial(specialId))
                return InternalServerError();

            return Ok();
        }
    }
}
