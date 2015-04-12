
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;
    using System.Collections.Generic;

    public class RssFeedResponseDto : ResponseBase
    {
        public List<RssFeedModel> SocialFeeds { get; set; }
    }
}