
namespace NJFairground.Web.Models
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Newtonsoft.Json;
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Data.Implementation;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models.Base;
    using NJFairground.Web.Utilities;

    public class PageItemModel : BaseModel
    {
        public int PageItemId { get; set; }
        [JsonIgnore]
        public int PageId { get; set; }
        public string PageHeaderText { get; set; }
        public string PageSubHeaderText { get; set; }
        [JsonIgnore]
        public string PageItemImage { get; set; }
        public string PageItemImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(this.PageItemImage) ?
                    CommonUtility.ResolveServerUrl("~/Styles/Images/logo_nj.png", false)
                    : 
                    CommonUtility.ResolveServerUrl(string.Format("{0}{1}",
                    CommonUtility.GetAppSetting<string>("UploadFolderItemImagePath"), this.PageItemImage), false);
            }
        }
        public string PageItemDetailText { get; set; }
        [JsonIgnore]
        public string PageItemSubDetail { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
        public int ItemOrder { get; set; }
        [JsonIgnore]
        public BannerModel PageBanner { get; set; }
    }

    public class CustomPageItemBannerResolver : ValueResolver<NJFairground.Web.Data.Context.PageItem, Banner>
    {
        private readonly IBannerDataRepository _bannerDataRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageItemBannerResolver"/> class.
        /// </summary>
        public CustomPageItemBannerResolver()
        {
            IDependencyResolver resolver = DependencyResolver.Current;
            this._bannerDataRepository = resolver.GetService<BannerDataRepository>(); ;
        }

        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
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

