using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NJFairground.Web.Controllers
{
    public class DetailsController : Controller
    {
        //
        // GET: /Details/

        public ActionResult Index(int PageId,int PageItemId)
        {
            ViewBag.PageId = PageId;
            ViewBag.PageItemId = PageItemId;
            return View("Index.mobile");
        }

    }
}
