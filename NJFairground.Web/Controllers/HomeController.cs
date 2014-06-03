
namespace NJFairground.Web.Controllers
{
    using System.Web.Mvc;
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View("Index.mobile");
        }
    }
}
