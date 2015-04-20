
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class UserImageResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserImageResponseDto" /> class.
        /// </summary>
        public UserImageResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritePageResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public UserImageResponseDto(string requestToken)
            : base(requestToken)
        {
        }

        public UserInfoModel UserInfo { get; set; }
    }
}