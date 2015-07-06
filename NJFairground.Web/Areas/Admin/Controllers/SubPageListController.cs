using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NJFairground.Web.Data.Interface;
using NJFairground.Web.Models;
using NJFairground.Web.Utilities;

namespace NJFairground.Web.Areas.Admin.Controllers
{
    public class SubPageListController : Controller
    {
        private readonly IPageDataRepository _pageDataRepository;
        private readonly IPageItemDataRepository _pageItemDataRepository;

        public SubPageListController(IPageDataRepository pageDataRepository,
            IPageItemDataRepository pageItemDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
            this._pageItemDataRepository = pageItemDataRepository;
        }

        public ActionResult Index()
        {
            try
            {
                var pages = this._pageDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                ViewBag.Pages = new SelectList(pages, "PageId", "PageDesc");
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return View();
        }

        public ActionResult GetPageItems(int pageId)
        {
            try
            {
                var pageItems = this._pageItemDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                    && x.PageId.Equals(pageId), x => x.ItemOrder, true).ToList();

                if (!pageItems.IsEmptyCollection())
                {
                    return PartialView("SubItems", pageItems);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Content(string.Empty);
        }

        [HttpGet]
        public ActionResult Edit(int pageItemId)
        {
            try
            {
                var pageItem = this._pageItemDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                    && x.PageItemId.Equals(pageItemId), x => x.ItemOrder, true).FirstOrDefaultCustom();

                if (pageItem != null)
                {
                    return PartialView("SubItemDetail", pageItem);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Content(string.Empty);
        }

        [HttpPost]
        public ActionResult Edit(PageItemModel pageItem)
        {
            try
            {
                if (pageItem != null)
                {
                    this._pageItemDataRepository.Update(pageItem);
                    return RedirectToAction("GetPageItems", new { pageId = pageItem.PageId });
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