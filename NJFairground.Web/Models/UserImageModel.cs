

namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Models.Base;
    using Newtonsoft.Json;

    public class UserImageModel : BaseModel
    {
        [JsonIgnore]
        public int UserImageId { get; set; }
        [JsonIgnore]
        public int UserKey { get; set; }
        public string UserImageUrl { get; set; }
        public string ImageUrl { get { return CommonUtility.ResolveServerUrl(this.UserImageUrl, false); } set { } }
        [JsonIgnore]
        public int StatusId { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }

        public bool IsFavorite { get; set; }
    }
}