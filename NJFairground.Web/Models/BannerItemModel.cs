

namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Models.Base;

    public class BannerItemModel:BaseModel
    {
        public int BannerItemId { get; set; }
        public int BannerId { get; set; }
        public string ImageUrl { get; set; }
        public string SponsorUrl { get; set; }
        public int Position { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}