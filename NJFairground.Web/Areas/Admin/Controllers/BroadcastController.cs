

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNet.SignalR;
    using NJFairground.Web.Utilities.TaskScheduler.Hubs;
    using System.Web.Mvc;
    using System;

    public class BroadcastController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Broadcasts the announcement.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BroadcastAnnouncement(string msg)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AnnouncementHub>();
            context.Clients.All.GetAnnouncements(DateTime.Now.ToString("dd/MMM/yyyy"), msg);
            return Content(msg);
        }
    }
}