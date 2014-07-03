

namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;
    using System;

    public class EventModel : BaseModel
    {
        public int EventId { get; set; }
        public int PageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventTitle { get; set; }
        public string EventDesc { get; set; }
        public DateTime CreatedOn { get; set; }
        public int StatusId { get; set; }
    }
}