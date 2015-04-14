

namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;

    public class FavoritePageRequestDto : RequestBase
    {
        public int UserKey { get; set; }
        public int PageItemId { get; set; }
    }
}