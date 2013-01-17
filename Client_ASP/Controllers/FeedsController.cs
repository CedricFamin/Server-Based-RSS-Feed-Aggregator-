using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client_ASP.Models;
using Client_ASP.FeedServ;
using System.Diagnostics;

namespace Client_ASP.Controllers
{
    [Authorize]
    public class FeedsController : Controller
    {
        public ActionResult List()
        {
            FeedsService fs = new FeedsService();
            WebResultOfArrayOfChannelMeg_PnYqa feeds = fs.GetFeeds(Request.Cookies["AuthKey"].Value);
            if (feeds.ErrorCode == FeedServ.WebResultErrorCodeList.SUCCESS)
            {
                ViewBag.Feeds = feeds.Value;
            }
            return View();
        }

        public ActionResult Feed(Channel channel)
        {
            FeedsService fs = new FeedsService();
            WebResultOfArrayOfItemMeg_PnYqa items = fs.GetFeedItems(Request.Cookies["AuthKey"].Value, channel);
            if (items.ErrorCode == FeedServ.WebResultErrorCodeList.SUCCESS)
            {
                Item[] chanitems = items.Value;
                ViewBag.Items = chanitems;
            }
            return View();
        }

        [ValidateInput(false)]
        public ActionResult Item(Item item)
        {
            FeedsService fs = new FeedsService();
            fs.ReadItem(Request.Cookies["AuthKey"].Value, item);
            ViewBag.ItemDatas = item;
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
            if (ModelState.IsValid)
            {
                FeedsService fs = new FeedsService();
                WebResultOfChannelMeg_PnYqa addnew = fs.AddNewFeed(Request.Cookies["AuthKey"].Value, model.Uri);
                if (addnew.ErrorCode == FeedServ.WebResultErrorCodeList.SUCCESS)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

        //
        // GET: /Feeds/Delete/5
 
        public ActionResult Delete(Channel channel)
        {
            FeedsService fs = new FeedsService();
            fs.UnfollowFeed(Request.Cookies["AuthKey"].Value, channel);
            return RedirectToAction("List");
        }
    }
}
