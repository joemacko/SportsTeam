using ElevenFiftySports.Models.SpecialModels;
using ElevenFiftySports.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenFiftySports.Controllers
{
    [Authorize]
    public class SpecialController : ApiController
    {
        private SpecialService CreateSpecialService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var specialService = new SpecialService(userId);
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

        public IHttpActionResult Put(SpecialEdit special)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSpecialService();

            if (!service.UpdateSpecial(special))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete([FromUri] int specialId)
        {
            var service = CreateSpecialService();

            if (!service.DeleteSpecial(specialId))
                return InternalServerError();

            return Ok();
        }
    }
}
