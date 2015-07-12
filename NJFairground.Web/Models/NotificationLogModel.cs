
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;
    using System;

    public class NotificationLogModel : BaseModel
    {
        public int NotificationLogId { get; set; }
        public string NotifiactionToken { get; set; }
        public int DeviceRegistryId { get; set; }
        public string DeviceId { get; set; }
        public int PageItemId { get; set; }
        public int StatusId { get; set; }
        public bool IsRead { get; set; }
        public DateTime NotifiedOn { get; set; }
        public DateTime? ReadOn { get; set; }

        public DeviceRegistryModel DeviceRegistry { get; set; }
        public PageItemModel PageItem { get; set; }
    }
}