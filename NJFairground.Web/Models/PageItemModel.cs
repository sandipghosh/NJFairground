
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;
    using NJFairground.Web.Utilities;
    using System.Web;
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
                return CommonUtility.ResolveServerUrl(string.Format("{0}{1}",
                    CommonUtility.GetAppSetting<string>("UploadFolderItemImagePath"), this.PageItemImage), false);
            }
        }
        public string PageItemDetailText { get; set; }
        public string PageItemSubDetail { get; set; }
        public int StatusId { get; set; }
        public int ItemOrder { get; set; }
    }
}

