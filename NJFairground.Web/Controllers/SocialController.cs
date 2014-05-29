using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NJFairground.Web.Controllers
{
    public class SocialController : Controller
    {
        //
        // GET: /Social/

        public ActionResult Index()
        {
            List<string> NavItems = new List<string>();
            NavItems.Add("The Fair");
            NavItems.Add("Fun");
            NavItems.Add("Info");
            NavItems.Add("Social");
            NavItems.Add("Map");

            ViewBag.NavBarItems = NavItems;
            return View();
        }

    }
}
