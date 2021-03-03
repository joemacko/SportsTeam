using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using ElevenFiftySports.Services;

namespace ElevenFiftySports.Controllers
{
    [System.Web.Http.Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        public IHttpActionResult Get()
        {
            ProductService productService = CreateProductService();
            var products = productService.GetProducts();
            return Ok(products);
        }



        public ActionResult Index()
        {
            return View();
        }
    }
}