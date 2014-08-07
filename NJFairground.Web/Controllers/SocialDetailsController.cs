
namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using System.Web.Mvc;

    public class SocialDetailsController : BaseController
    {
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
