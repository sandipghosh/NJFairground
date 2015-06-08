
namespace NJFairground.Web.DTO.RequestDto
{
    using System.Web.Http.ModelBinding;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.MapperConfig;
    using NJFairground.Web.Models;

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