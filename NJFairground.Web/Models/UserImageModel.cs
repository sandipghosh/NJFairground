

namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Models.Base;

    public class UserImageModel : BaseModel
    {
        public int UserImageId { get; set; }
        public int UserKey { get; set; }
        public string UserImageUrl { get; set; }
        public string ImageUrl { get { return CommonUtility.ResolveServerUrl(this.UserImageUrl, false); } }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool IsFavorite { get; set; }
    }
}