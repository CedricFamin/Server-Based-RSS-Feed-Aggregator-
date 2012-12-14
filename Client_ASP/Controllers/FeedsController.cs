using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client_ASP.Models;

namespace Client_ASP.Controllers
{
    [Authorize]
    public class FeedsController : Controller
    {
        public FeedsController() : base()
        {
            
        }
        //
        // GET: /Feeds/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Feeds/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Feeds/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Feeds/Create

        [HttpPost]
        public ActionResult Create(CreateFeedModel model)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Feeds/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Feeds/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Feeds/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Feeds/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
