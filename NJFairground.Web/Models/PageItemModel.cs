
namespace NJFairground.Web.Models
{
    using AutoMapper;
    using System.Linq;
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Models.Base;
    using NJFairground.Web.Utilities;

    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Data.Implementation;
    using System.Web.Mvc;

    public class PageItemModel : BaseModel
    {
        public int PageItemId { get; set; }
        public int PageId { get; set; }
        public string PageHeaderText { get; set; }
        public string PageSubHeaderText { get; set; }
        public string PageItemImage { get; set; }
        public string PageItemImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(this.PageItemImage) ? "" : CommonUtility.ResolveServerUrl(string.Format("{0}{1}",
                    CommonUtility.GetAppSetting<string>("UploadFolderItemImagePath"), this.PageItemImage), false);
            }
        }
        public string PageItemDetailText { get; set; }
        public string PageItemSubDetail { get; set; }
        public int StatusId { get; set; }
        public int ItemOrder { get; set; }
        public BannerModel PageBanner { get; set; }
    }

    public class CustomPageItemBannerResolver : ValueResolver<NJFairground.Web.Data.Context.PageItem, Banner>
    {
        private readonly IBannerDataRepository _bannerDataRepository;
        public CustomPageItemBannerResolver()
        {
            IDependencyResolver resolver = DependencyResolver.Current;
            this._bannerDataRepository = resolver.GetService<BannerDataRepository>(); ;
        }

        protected override Banner ResolveCore(NJFairground.Web.Data.Context.PageItem source)
        {
            Banner banner = new Banner();
            if (!source.PageBanners.IsEmptyCollection())
                banner = source.PageBanners.FirstOrDefault().Banner;
            else
            {
                var bannerModel = this._bannerDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                    && x.IsDefault == true).FirstOrDefault();
                banner = Mapper.Map<BannerModel, Banner>(bannerModel);
            }

            return banner;
        }
    }
}

