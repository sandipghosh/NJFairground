
namespace NJFairground.Web.Controllers
{
    using System.Web.Mvc;
    public class HomeController : Controller
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
    }
}
