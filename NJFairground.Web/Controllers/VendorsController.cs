
namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class VendorsController : BaseController
    {
        private readonly IPageDataRepository _pageDataRepository;
        private readonly IPageItemDataRepository _pageItemDataRepository;

        public VendorsController(IPageDataRepository pageDataRepository, IPageItemDataRepository pageItemDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
            this._pageItemDataRepository = pageItemDataRepository;
        }
        public ActionResult Index()
        {
            List<int> pages = new List<int>() { (int)Page.CraftTent, (int)Page.CommercialInfo, (int)Page.CommercialCraftTent };
            List<PageModel> pageItems = this._pageDataRepository
                .GetList(x => pages.Contains(x.PageId) && x.StatusId.Equals((int)StatusEnum.Active)).ToList();

            ViewBag.PageHeaderText = "2014 Vendors";
            return View("Index.mobile", pageItems);
        }

        public ActionResult VendorDetail(int pageId, string pageHeader)
        {
            List<PageItemModel> pageItems = this._pageItemDataRepository
                .GetList(x => x.PageId == pageId
                    && x.StatusId.Equals((int)StatusEnum.Active), y => y.ItemOrder, true).ToList();

            ViewBag.PageHeaderName = pageHeader;
            return View("VendorDetail.mobile", pageItems);
        }
    }
}
