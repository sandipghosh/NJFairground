
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
    public class DailyHighlightsController : Controller
    {
        private readonly IPageItemDataRepository _pageItemDataRepository;

        public DailyHighlightsController(IPageItemDataRepository pageItemDataRepository)
        {
            this._pageItemDataRepository = pageItemDataRepository;
        }
        //
        // GET: /DailyHighlights/

        public ActionResult Index()
        {
            List<PageItemModel> pageItems = this._pageItemDataRepository.GetList(x => x.PageId == Convert.ToInt32(Page.DailyHighlights) && x.StatusId == 1).ToList();
            return View("Index.mobile", pageItems);
        }

    }
}
