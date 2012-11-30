using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client_ASP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "RSS Feed Aggregator";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
