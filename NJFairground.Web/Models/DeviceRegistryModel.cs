
namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Models.Base;

    public class DeviceRegistryModel : BaseModel
    {
        public int DeviceRegistryId { get; set; }
        public string DeviceId { get; set; }
        public int DeviceType { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}