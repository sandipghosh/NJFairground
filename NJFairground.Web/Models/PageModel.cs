
namespace NJFairground.Web.Models
{
    using AutoMapper;
    using Newtonsoft.Json;
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Data.Implementation;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models.Base;
    using NJFairground.Web.Utilities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class PageModel : BaseModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageDesc { get; set; }

        [AllowHtml]
        public string PageContent { get; set; }
        public string PageImage { get; set; }

        [JsonIgnore]
        public BannerModel PageBanner { get; set; }

        public string PageImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(this.PageImage) ? "" : CommonUtility.ResolveServerUrl(string.Format("{0}{1}",
                    CommonUtility.GetAppSetting<string>("UploadFolderItemImagePath"), this.PageImage), false);
            }
        }

        [JsonIgnore]
        public int StatusId { get; set; }

        [JsonIgnore]
        public IEnumerable<PageItemModel> PageItems { get; set; }
    }

    public class CustomPageBannerResolver : ValueResolver<NJFairground.Web.Data.Context.Page, Banner>
    {
        private readonly IBannerDataRepository _bannerDataRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageBannerResolver"/> class.
        /// </summary>
        public CustomPageBannerResolver()
        {
            IDependencyResolver resolver = DependencyResolver.Current;
            this._bannerDataRepository = resolver.GetService<BannerDataRepository>(); ;
        }

        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override Banner ResolveCore(NJFairground.Web.Data.Context.Page source)
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