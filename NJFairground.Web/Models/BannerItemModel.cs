

namespace NJFairground.Web.Models
{
    using Newtonsoft.Json;
    using NJFairground.Web.Models.Base;
    using System;

    public class BannerItemModel : BaseModel
    {
        [JsonIgnore]
        public int BannerItemId { get; set; }
        [JsonIgnore]
        public int BannerId { get; set; }
        public string ImageUrl { get; set; }
        public string SponsorUrl { get; set; }
        public int Position { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
    }
}