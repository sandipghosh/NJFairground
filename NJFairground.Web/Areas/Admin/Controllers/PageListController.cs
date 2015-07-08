

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;

    public class PageListController : Controller
    {
        private readonly IPageDataRepository _pageDataRepository;

        public PageListController(IPageDataRepository pageDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
        }

        public ActionResult Index()
        {
            IList<PageModel> pages = new List<PageModel>();
            try
            {
                pages = this.GetPages();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return View(pages);
        }

        private IList<PageModel> GetPages()
        {
            IList<PageModel> pages = new List<PageModel>();
            try
            {
                pages = this._pageDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return pages;
        }

        [HttpGet]
        public ActionResult Edit(int pageId)
        {
            try
            {
                var page = this._pageDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                    && x.PageId.Equals(pageId)).FirstOrDefaultCustom();

                if (page != null)
                {
                    return PartialView("PageDetail", page);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Content(string.Empty);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(PageModel page)
        {
            try
            {
                if (page != null)
                {
                    this._pageDataRepository.Update(page);
                    return RedirectToAction("GetPages");
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Content(string.Empty);
        }
    }
}