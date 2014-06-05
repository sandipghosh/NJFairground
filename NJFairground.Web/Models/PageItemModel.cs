
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class PageItemModel : BaseModel
    {
        public int PageItemId { get; set; }
        public int PageId { get; set; }
        public int PageItemType { get; set; }
        public string PageHeaderText { get; set; }
        public string PageSubHeaderText { get; set; }
        public string PageItemImage { get; set; }
        public string PageItemDetailText { get; set; }
        public string PageItemSubDetail { get; set; }
        public int StatusId { get; set; }
    }
}

