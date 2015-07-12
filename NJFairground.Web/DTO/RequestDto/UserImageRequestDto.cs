
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Extensions.Bindings;
    using NJFairground.Web.Models;
    using System.Web.Http.ModelBinding;

    [ModelBinder(typeof(UserImageRequestCustomBinder))]
    public class UserImageRequestDto : RequestBase
    {
        public UserImageRequestDto()
        {
            this.UserInfo = new UserInfoModel();
        }

        public UserInfoModel UserInfo { get; set; }
        public int UserImageId { get; set; }
    }
}