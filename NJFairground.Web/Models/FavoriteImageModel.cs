

namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Models.Base;
    using Newtonsoft.Json;

    public class FavoriteImageModel : BaseModel
    {
        [JsonIgnore]
        public int FavoriteImageId { get; set; }
        public int UserImageId { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
    }
}