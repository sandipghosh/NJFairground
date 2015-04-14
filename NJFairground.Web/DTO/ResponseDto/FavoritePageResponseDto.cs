
namespace NJFairground.Web.DTO.ResponseDto
{
    using System.Collections.Generic;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class FavoritePageResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritePageResponseDto"/> class.
        /// </summary>
        public FavoritePageResponseDto()
        {
            this.FavoritePages = new List<PageItemModel>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritePageResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public FavoritePageResponseDto(string requestToken)
            : base(requestToken)
        {
            this.FavoritePages = new List<PageItemModel>();
        }

        /// <summary>
        /// Gets or sets the favorite pages.
        /// </summary>
        /// <value>
        /// The favorite pages.
        /// </value>
        public List<PageItemModel> FavoritePages { get; set; }
    }
}