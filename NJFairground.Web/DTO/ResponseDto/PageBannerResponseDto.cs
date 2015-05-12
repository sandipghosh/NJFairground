
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class PageBannerResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritePageResponseDto"/> class.
        /// </summary>
        public PageBannerResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritePageResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public PageBannerResponseDto(string requestToken)
            : base(requestToken)
        {
        }

        public BannerModel Banner { get; set; }
    }
}