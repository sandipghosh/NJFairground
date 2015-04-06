
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;

    public class PageItemRequestDto : RequestBase
    {
        public int PageItemId { get; set; }
        public int PageId { get; set; }
    }
}