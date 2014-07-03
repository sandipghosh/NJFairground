
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;

    public class PageItemModel : BaseModel
    {
        public int PageItemId { get; set; }
        public int PageId { get; set; }
        public string PageHeaderText { get; set; }
        public string PageSubHeaderText { get; set; }
        public string PageItemImage { get; set; }
        public string PageItemDetailText { get; set; }
        public string PageItemSubDetail { get; set; }
        public int StatusId { get; set; }
    }
}

