
namespace NJFairground.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Models;

    public class InfoController : Controller
    {

         private readonly IPageItemDataRepository _pageItemDataRepository;

         public InfoController(IPageItemDataRepository pageItemDataRepository)
        {
            this._pageItemDataRepository = pageItemDataRepository;
        }

        //
        // GET: /Info/

        public ActionResult Index()
        {
            List<string> NavItems=new List<string>();
            NavItems.Add("The Fair");
            NavItems.Add("Fun");
            NavItems.Add("Info");
            NavItems.Add("Social");
            NavItems.Add("Map");

            ViewBag.NavBarItems = NavItems;


            var pageItems = this._pageItemDataRepository.GetList(x => x.PageId ==Convert.ToInt32(Page.Info)).ToList();
            return View("Index.mobile", pageItems);
        }

    }
}
