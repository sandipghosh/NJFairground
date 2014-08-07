

namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using System.Web.Mvc;

    public class FairController : BaseController
    {
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {
            return View("Index.mobile");
        }
    }
}
