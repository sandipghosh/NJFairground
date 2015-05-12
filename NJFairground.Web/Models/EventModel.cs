

namespace NJFairground.Web.Models
{
    using Newtonsoft.Json;
    using NJFairground.Web.Models.Base;
    using System;

    public class EventModel : BaseModel
    {
        [JsonIgnore]
        public int EventId { get; set; }
        public int PageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventTitle { get; set; }
        public string EventDesc { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
    }
}