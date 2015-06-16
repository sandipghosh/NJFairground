

namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Utilities;

    public class SplashImageResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashImageResponseDto"/> class.
        /// </summary>
        public SplashImageResponseDto()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplashImageResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public SplashImageResponseDto(string requestToken)
            : base(requestToken)
        {

        }

        public int SplashImageId { get; set; }

        private string _imageUrl;
        public string ImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(this._imageUrl) ? string.Empty :
                   CommonUtility.ResolveServerUrl(this._imageUrl, false);
            }
            set { this._imageUrl = value; }
        }

        /// <summary>
        /// Gets or sets the redirection URL.
        /// </summary>
        /// <value>
        /// The redirection URL.
        /// </value>
        public string RedirectionUrl { get; set; }
    }
}