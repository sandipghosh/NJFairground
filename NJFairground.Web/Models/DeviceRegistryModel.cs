
namespace NJFairground.Web.Models
{
    using System;
    using System.Collections.Generic;
    using NJFairground.Web.Models.Base;
    using Newtonsoft.Json;

    public class DeviceRegistryModel : BaseModel
    {
        public int DeviceRegistryId { get; set; }
        public string DeviceId { get; set; }
        public int DeviceType { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<NotificationLogModel> NotificationLogs { get; set; }
    }
}