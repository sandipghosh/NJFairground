
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class UserImageRequestDto : RequestBase
    {
        public UserInfoModel UserInfo { get; set; }
        public int UserImageId { get; set; }
    }
}