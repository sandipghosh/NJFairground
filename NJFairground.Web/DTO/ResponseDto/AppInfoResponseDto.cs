
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class AppInfoResponseDto : ResponseBase
    {
        public AppInfoResponseDto()
        {
        }

        public AppInfoResponseDto(string requestToken)
            : base(requestToken)
        {

        }
        public AppInfoModel AppInfo { get; set; }
    }
}