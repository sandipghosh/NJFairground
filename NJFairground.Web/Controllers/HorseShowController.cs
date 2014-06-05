using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NJFairground.Web.Controllers
{
    public class HorseShowController : Controller
    {
        //
        // GET: /HorseShow/

        public ActionResult Index()
        {
            return View("Index.mobile");
        }

    }
}
