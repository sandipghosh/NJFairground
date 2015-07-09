

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class PageListController : Controller
    {
        private readonly IPageDataRepository _pageDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageListController"/> class.
        /// </summary>
        /// <param name="pageDataRepository">The page data repository.</param>
        public PageListController(IPageDataRepository pageDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
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

        /// <summary>
        /// Gets the pages.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Edits the specified page identifier.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
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

        /// <summary>
        /// Edits the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        [OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
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