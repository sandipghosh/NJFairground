using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NJFairground.Web.Controllers
{
    public class FunController : Controller
    {
        //
        // GET: /Fun/

        public ActionResult Index()
        {
            List<string> NavItems = new List<string>();
            NavItems.Add("Fun");
           
            ViewBag.NavBarItems = NavItems;

            return View("Index.mobile");
        }

    }
}
