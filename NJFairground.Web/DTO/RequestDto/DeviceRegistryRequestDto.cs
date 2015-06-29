
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;

    public class DeviceRegistryRequestDto : RequestBase
    {
        public string DeviceId { get; set; }
        public int DeciceType { get; set; }
    }
}