

namespace NJFairground.Web.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using NJFairground.Web.Models.Base;

    public class BannerModel:BaseModel
    {
        [JsonIgnore]
        public int BannerId { get; set; }
        [JsonIgnore]
        public bool IsDefault { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
        [JsonIgnore]
        public bool IsSplashImage { get; set; }

        public List<BannerItemModel> BannerItems { get; set; }
    }
}