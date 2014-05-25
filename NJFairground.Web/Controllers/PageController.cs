

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

    public class PageController : Controller
    {
        private readonly IPageDataRepository _pageDataRepository;

        public PageController(IPageDataRepository pageDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
        }

        public ActionResult Index()
        {
            try
            {
                //var pages = this._pageDataRepository.GetList().ToList();

                //var pages = this._pageDataRepository.GetList(x => x.PageName.EndsWith("2") && x.StatusId == 1).ToList();

                //PageModel page = new PageModel { PageId = 5, PageName = "Page5", PageDesc = "Page6", StatusId = 1 };
                //this._pageDataRepository.Insert(page);

                var pages = this._pageDataRepository.GetList(x => x.StatusId == 1, x => x.PageId, false).ToList();


                

                return View(pages);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return null;
        }

    }
}
