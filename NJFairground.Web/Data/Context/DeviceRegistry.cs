//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NJFairground.Web.Data.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeviceRegistry
    {
        public DeviceRegistry()
        {
            this.NotificationLogs = new HashSet<NotificationLog>();
        }
    
        public int DeviceRegistryId { get; set; }
        public string DeviceId { get; set; }
        public int DeviceType { get; set; }
        public int StatusId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }
    }
}
