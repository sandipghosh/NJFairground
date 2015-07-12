
namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class NotificationReadRequestDto : RequestBase
    {
        public string NotifiactionToken { get; set; }
        public string DeviceId { get; set; }
    }
}