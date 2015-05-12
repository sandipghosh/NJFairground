
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;

    public class PageItemRequestDto : RequestBase
    {
        public int PageItemId { get; set; }
        public int PageId { get; set; }

        private bool _RespondWithBanner = false;
        public bool RespondWithBanner { get { return _RespondWithBanner; } set { _RespondWithBanner = value; } }
    }
}