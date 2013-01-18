using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client_ASP.Models;
using Client_ASP.FeedServ;
using System.Diagnostics;
using System.Web.Security;

namespace Client_ASP.Controllers
{
    [Authorize]
    public class FeedsController : Controller
    {
        public ActionResult List()
        {
            FormsIdentity ident = User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = ident.Ticket;
            string AuthKey = ticket.UserData;

            FeedsService fs = new FeedsService();
            WebResultOfArrayOfChannelMeg_PnYqa feeds = fs.GetFeeds(AuthKey);
            if (feeds.ErrorCode == FeedServ.WebResultErrorCodeList.SUCCESS)
            {
                ViewBag.Feeds = feeds.Value;
            }
            return View();
        }

        public ActionResult Public()
        {
            FeedsService fs = new FeedsService();
            WebResultOfArrayOfChannelMeg_PnYqa feeds = fs.GetAllFeeds();
            if (feeds.ErrorCode == FeedServ.WebResultErrorCodeList.SUCCESS)
            {
                ViewBag.Feeds = feeds.Value;
            }
            return View();
        }

        public ActionResult Feed(Channel channel)
        {
            FormsIdentity ident = User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = ident.Ticket;
            string AuthKey = ticket.UserData;

            FeedsService fs = new FeedsService();
            WebResultOfArrayOfItemMeg_PnYqa items = fs.GetFeedItems(AuthKey, channel);
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
            FormsIdentity ident = User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = ident.Ticket;
            string AuthKey = ticket.UserData;
            
            FeedsService fs = new FeedsService();
            fs.ReadItem(AuthKey, item);
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
                FormsIdentity ident = User.Identity as FormsIdentity;
                FormsAuthenticationTicket ticket = ident.Ticket;
                string AuthKey = ticket.UserData;
                try
                {
                    FeedsService fs = new FeedsService();
                    WebResultOfChannelMeg_PnYqa addnew = fs.AddNewFeed(AuthKey, model.Uri);
                    if (addnew.ErrorCode == FeedServ.WebResultErrorCodeList.SUCCESS)
                    {
                        return RedirectToAction("List");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.ErrorFeed = "Could not add the feed";
                }
            }
            return View(model);
        }

        //
        // GET: /Feeds/Delete/5
 
        public ActionResult Delete(Channel channel)
        {
            FormsIdentity ident = User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = ident.Ticket;
            string AuthKey = ticket.UserData;
            FeedsService fs = new FeedsService();
            fs.UnfollowFeed(AuthKey, channel);
            return RedirectToAction("List");
        }
    }
}
