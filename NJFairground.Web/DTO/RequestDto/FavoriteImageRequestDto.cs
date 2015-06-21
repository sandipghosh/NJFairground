
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class FavoriteImageRequestDto : RequestBase
    {
        public FavoriteImageRequestDto()
        {
            this.UserInfo = new UserInfoModel();
        }

        public UserInfoModel UserInfo { get; set; }
        public int UserImageId { get; set; }
    }
}