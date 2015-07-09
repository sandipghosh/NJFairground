

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        // GET: Admin/Home
        [OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {
            return View();
        }
    }
}