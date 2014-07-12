

namespace NJFairground.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Models;

    public class CircusHollywoodController : BaseController
    {
        //
        // GET: /CircusHollywood/
        // <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Details", new { PageId = Convert.ToInt32(Page.CircusHollywood), PageItemId = 2201 });
        }

    }
}
