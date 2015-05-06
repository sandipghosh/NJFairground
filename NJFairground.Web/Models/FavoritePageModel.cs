

namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Models.Base;
    using Newtonsoft.Json;

    public class FavoritePageModel:BaseModel
    {
        [JsonIgnore]
        public int FavoritePageId { get; set; }
        [JsonIgnore]
        public int UserKey { get; set; }
        public int PageId { get; set; }
        public int PageItemId { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }

        public PageItemModel PageItem { get; set; }
    }
}