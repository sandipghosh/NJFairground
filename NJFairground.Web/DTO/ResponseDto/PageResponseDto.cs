
namespace NJFairground.Web.DTO.ResponseDto
{
    using System.Collections.Generic;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class PageResponseDto : ResponseBase
    {
        public PageModel Page { get; set; }
        public IEnumerable<PageModel> Pages { get; set; }
    }
}