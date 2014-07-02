
namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using System.Linq;
    using System.Web.Mvc;

    public class DetailsController : BaseController
    {
        private readonly IPageItemDataRepository _pageItemDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsController"/> class.
        /// </summary>
        /// <param name="pageItemDataRepository">The page item data repository.</param>
        public DetailsController(IPageItemDataRepository pageItemDataRepository)
        {
            this._pageItemDataRepository = pageItemDataRepository;
        }

        /// <summary>
        /// Indexes the specified page identifier.
        /// </summary>
        /// <param name="PageId">The page identifier.</param>
        /// <param name="PageItemId">The page item identifier.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index(int PageId, int PageItemId)
        {
            ViewBag.PageId = PageId;
            ViewBag.PageItemId = PageItemId;
            PageItemModel pageItems = this._pageItemDataRepository.GetList(x => x.PageId == PageId && x.PageItemId == PageItemId).FirstOrDefault();
            return View("Index.mobile", pageItems);
        }

    }
}
