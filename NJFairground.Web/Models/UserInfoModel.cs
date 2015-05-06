

namespace NJFairground.Web.Models
{
    using Newtonsoft.Json;
    using NJFairground.Web.Models.Base;
    using System;
    using System.Collections.Generic;

    public class UserInfoModel : BaseModel
    {
        public UserInfoModel()
        {
            this.UserImages = new List<UserImageModel>();
            this.FavoritePages = new List<FavoritePageModel>();
        }

        public int UserKey { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }

        public List<UserImageModel> UserImages { get; set; }
        public List<FavoritePageModel> FavoritePages { get; set; }
    }
}