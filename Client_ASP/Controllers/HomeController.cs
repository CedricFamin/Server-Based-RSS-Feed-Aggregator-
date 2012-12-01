using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Client_ASP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Username"] != null)
                ViewBag.auth = true;
            else
                ViewBag.auth = false;

            ViewBag.Message = "RSS Feed Aggregator";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
