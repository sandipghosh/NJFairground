

namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;

    public class PageBannerModel : BaseModel
    {
        public int PageBannerId { get; set; }
        public int BannerId { get; set; }
        public int PageId { get; set; }
        public int PageItemId { get; set; }
        public int StatusId { get; set; }
    }
}