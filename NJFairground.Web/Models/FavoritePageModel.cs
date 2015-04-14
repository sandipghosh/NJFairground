

namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Models.Base;

    public class FavoritePageModel:BaseModel
    {
        public int FavoritePageId { get; set; }
        public int UserKey { get; set; }
        public int PageId { get; set; }
        public int PageItemId { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public PageItemModel PageItem { get; set; }
    }
}