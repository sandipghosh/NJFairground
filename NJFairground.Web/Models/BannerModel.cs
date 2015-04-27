

namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Models.Base;

    public class BannerModel:BaseModel
    {
        public int BannerId { get; set; }
        public bool IsDefault { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}