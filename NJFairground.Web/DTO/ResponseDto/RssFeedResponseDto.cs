
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;
    using System.Collections.Generic;

    public class RssFeedResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedResponseDto"/> class.
        /// </summary>
        public RssFeedResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public RssFeedResponseDto(string requestToken)
            : base(requestToken)
        {
        }

        /// <summary>
        /// Gets or sets the social feeds.
        /// </summary>
        /// <value>
        /// The social feeds.
        /// </value>
        public List<RssFeedModel> SocialFeeds { get; set; }
    }
}