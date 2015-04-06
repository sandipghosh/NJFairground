
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;

    public class PageRequestDto : RequestBase
    {
        public PageRequestDto()
            : base()
        {

        }

        public int PageId { get; set; }
    }
}