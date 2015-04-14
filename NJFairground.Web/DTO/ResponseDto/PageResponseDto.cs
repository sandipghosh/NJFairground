
namespace NJFairground.Web.DTO.ResponseDto
{
    using System.Collections.Generic;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class PageResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageResponseDto"/> class.
        /// </summary>
        public PageResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public PageResponseDto(string requestToken)
            : base(requestToken)
        {
        }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public PageModel Page { get; set; }

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>
        /// The pages.
        /// </value>
        public IEnumerable<PageModel> Pages { get; set; }
    }
}