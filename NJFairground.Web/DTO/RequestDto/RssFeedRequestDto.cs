
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class RssFeedRequestDto : RequestBase
    {
        public FeedFor FeedRequestFor { get; set; }
    }
}