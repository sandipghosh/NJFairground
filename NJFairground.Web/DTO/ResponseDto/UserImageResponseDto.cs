
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;
    using System.Collections.Generic;

    public class UserImageResponseDto : ResponseBase
    {
        public UserImageResponseDto()
        {
            this.UserImages = new List<UserImageModel>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritePageResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public UserImageResponseDto(string requestToken)
            : base(requestToken)
        {
            this.UserImages = new List<UserImageModel>();
        }

        public UserInfoModel UserInfo { get; set; }
        public List<UserImageModel> UserImages { get; set; }
    }
}