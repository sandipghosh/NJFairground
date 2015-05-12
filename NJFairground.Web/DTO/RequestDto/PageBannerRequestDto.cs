
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;

    public class PageBannerRequestDto : RequestBase
    {
        public int PageId { get; set; }
        public int PageItemId { get; set; }
    }
}