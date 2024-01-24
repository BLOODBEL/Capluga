using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class HomeController : Controller
    {


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult About()
        {
          

            return View();
        }


        [HttpGet]
        public ActionResult Contact()
        {
           

            return View();
        }


        [HttpGet]
        public ActionResult Productos()
        {


            return View();
        }


        [HttpGet]
        public ActionResult services()
        {


            return View();
        }
    }
}