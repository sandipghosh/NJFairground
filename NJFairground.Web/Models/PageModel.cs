
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

    public class PageModel : BaseModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageDesc { get; set; }
        public string PageContent { get; set; }
        public string PageImage { get; set; }

        public BannerModel PageBanner { get; set; }

        public string PageImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(this.PageImage) ? "" : CommonUtility.ResolveServerUrl(string.Format("{0}{1}",
                    CommonUtility.GetAppSetting<string>("UploadFolderItemImagePath"), this.PageImage), false);
            }
        }
        public int StatusId { get; set; }
    }

    public class CustomPageBannerResolver : ValueResolver<NJFairground.Web.Data.Context.Page, Banner>
    {
        private readonly IBannerDataRepository _bannerDataRepository;
        public CustomPageBannerResolver()
        {
            IDependencyResolver resolver = DependencyResolver.Current;
            this._bannerDataRepository = resolver.GetService<BannerDataRepository>(); ;
        }

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