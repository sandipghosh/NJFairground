
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;
    using NJFairground.Web.Utilities;

    public class PageModel : BaseModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageDesc { get; set; }
        public string PageContent { get; set; }
        public string PageImage { get; set; }

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
}