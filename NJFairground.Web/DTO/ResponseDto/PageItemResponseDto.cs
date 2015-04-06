
namespace NJFairground.Web.DTO.ResponseDto
{
    using System.Collections.Generic;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class PageItemResponseDto : ResponseBase
    {
        public PageItemModel PageItem { get; set; }
        public IEnumerable<PageItemModel> PageItems { get; set; }
    }
}