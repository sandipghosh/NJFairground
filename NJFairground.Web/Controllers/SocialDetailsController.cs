using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using System.Linq;
    using System.Web.Mvc;

    public class SocialDetailsController : BaseController
    {
         
        //
        // GET: /SocialDetails/

        /// <summary>
        /// Indexes the specified page identifier.
        /// </summary>
        /// <param name="PageId">The page identifier.</param>
        /// <param name="PageItemId">The page item identifier.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index(string feedLink,string feedFor)
        {
            ViewBag.feedLink = feedLink;
            ViewBag.feedFor = feedFor;
            return View("Index.mobile");
        }

    }
}
