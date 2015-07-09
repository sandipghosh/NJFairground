

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web;
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

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Delete(int pageId)
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
                if (ModelState.IsValid)
                {
                    if (ControllerContext.HttpContext.Request.Files != null
                        && ControllerContext.HttpContext.Request.Files.Count > 0)
                    {
                        string imagePath = this.UploadImage(ControllerContext.HttpContext.Request.Files[0]);
                        page.PageImage = imagePath;
                    }
                    else
                    {
                        page.PageImage = string.Empty;
                    }
                    this._pageDataRepository.Update(page);
                    return JavaScript(string.Format("window.location.assign('{0}');", Url.Action("Index", "PageList")));
                }
                else
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
        /// Uploads the image.
        /// </summary>
        /// <param name="uploadedFile">The uploaded file.</param>
        /// <returns></returns>
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