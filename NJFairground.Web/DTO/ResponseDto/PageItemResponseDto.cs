
namespace NJFairground.Web.DTO.ResponseDto
{
    using System.Collections.Generic;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class PageItemResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageItemResponseDto"/> class.
        /// </summary>
        public PageItemResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageItemResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public PageItemResponseDto(string requestToken)
            : base(requestToken)
        {
        }

        /// <summary>
        /// Gets or sets the page item.
        /// </summary>
        /// <value>
        /// The page item.
        /// </value>
        public PageItemModel PageItem { get; set; }

        /// <summary>
        /// Gets or sets the page items.
        /// </summary>
        /// <value>
        /// The page items.
        /// </value>
        public IEnumerable<PageItemModel> PageItems { get; set; }
    }
}