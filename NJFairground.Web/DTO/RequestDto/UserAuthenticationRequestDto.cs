

namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class UserAuthenticationRequestDto : RequestBase
    {
        public UserAuthenticationRequestDto()
            : base()
        {

        }

        public UserInfoModel UserInfo { get; set; }
    }
}