
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class DeviceRegistryResponseDto : ResponseBase
    {
        public DeviceRegistryResponseDto()
        {
        }

        public DeviceRegistryResponseDto(string requestToken)
            : base(requestToken)
        {

        }
        public DeviceRegistryModel Device { get; set; }
    }
}