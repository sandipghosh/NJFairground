
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class FavoriteImageResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritePageResponseDto"/> class.
        /// </summary>
        public FavoriteImageResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritePageResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public FavoriteImageResponseDto(string requestToken)
            : base(requestToken)
        {
        }

        public UserInfoModel UserInfo { get; set; }
    }
}