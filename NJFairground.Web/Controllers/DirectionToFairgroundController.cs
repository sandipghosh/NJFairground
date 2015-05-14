

namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using System.Web.Mvc;

    public class DirectionToFairgroundController : BaseController
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {
            return View("Index.mobile");
        }

        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult GetDirection()
        {
            return View("GetDirection.mobile");
        }
    }
}
