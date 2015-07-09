

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Routing;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;

    public class SubPageListController : Controller
    {
        private readonly IPageDataRepository _pageDataRepository;
        private readonly IPageItemDataRepository _pageItemDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubPageListController"/> class.
        /// </summary>
        /// <param name="pageDataRepository">The page data repository.</param>
        /// <param name="pageItemDataRepository">The page item data repository.</param>
        public SubPageListController(IPageDataRepository pageDataRepository,
            IPageItemDataRepository pageItemDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
            this._pageItemDataRepository = pageItemDataRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
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

        /// <summary>
        /// Gets the page items.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns></returns>
        [HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
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

        /// <summary>
        /// Edits the specified page item identifier.
        /// </summary>
        /// <param name="pageItemId">The page item identifier.</param>
        /// <returns></returns>
        [HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
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

        /// <summary>
        /// Edits the specified page item.
        /// </summary>
        /// <param name="pageItem">The page item.</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Edit(PageItemModel pageItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pageItem.UpdatedOn = DateTime.Now;
                    if (ControllerContext.HttpContext.Request.Files != null
                        && ControllerContext.HttpContext.Request.Files.Count > 0)
                    {
                        string imagePath = this.UploadImage(ControllerContext.HttpContext.Request.Files[0]);
                        pageItem.PageItemImage = imagePath;
                    }
                    else
                    {
                        pageItem.PageItemImage = string.Empty;
                    }

                    this._pageItemDataRepository.Update(pageItem);
                    return RedirectToAction("GetPageItems", "SubPageList", new RouteValueDictionary(new { pageId = pageItem.PageId }));
                }
                else
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

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Add(int pageId)
        {
            try
            {
                PageItemModel pageItem = new PageItemModel()
                {
                    StatusId = (int)StatusEnum.Active,
                    PageId = pageId,
                    PageItemSubDetail = string.Empty,
                    CreatedOn = DateTime.Now,
                    ActivatedOn = DateTime.Now
                };
                IList<PageModel> pages = new List<PageModel>();
                pages = this._pageDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                ViewBag.Pages = new SelectList(pages, "PageId", "PageDesc");

                return PartialView("SubItemInsert", pageItem);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Content("");
        }

        /// <summary>
        /// Adds the specified page item.
        /// </summary>
        /// <param name="pageItem">The page item.</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        [OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Add(PageItemModel pageItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ControllerContext.HttpContext.Request.Files != null
                        && ControllerContext.HttpContext.Request.Files.Count > 0)
                    {
                        string imagePath = this.UploadImage(ControllerContext.HttpContext.Request.Files[0]);
                        pageItem.PageItemImage = imagePath;
                    }
                    else
                    {
                        pageItem.PageItemImage = string.Empty;
                    }
                    var newPageItem = this._pageItemDataRepository.InsertCustom(pageItem);
                    return RedirectToAction("GetPageItems", "SubPageList", new RouteValueDictionary(new { pageId = pageItem.PageId }));
                }
                else
                {
                    return PartialView("SubItemInsert", pageItem);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(pageItem);
            }
            return Content("");
        }

        private string UploadImage(HttpPostedFileBase uploadedFile)
        {
            try
            {
                uploadedFile.InputStream.Seek(0, SeekOrigin.Begin);
                using (Image image = Image.FromStream(uploadedFile.InputStream))
                {
                    string virtualPath = CommonUtility.GetAppSetting<string>("UploadFolderItemImagePath");
                    string filePath = Server.MapPath(virtualPath);

                    string fileName = string.Format("{0:N}.jpg", Guid.NewGuid());
                    string fullPath = Path.Combine(filePath, fileName);

                    image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return fileName;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(uploadedFile);
            }
            return string.Empty;
        }
    }
}