
namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using System.Web.Mvc;

    public class LandingPageController : BaseController
    {
        public ActionResult Index()
        {
            return View("Index.mobile");
        }

    }
}
