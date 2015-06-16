namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;
    using System;

    public class HitCounterModel : BaseModel
    {
        public int HitCounterId { get; set; }
        public int SponsorId { get; set; }
        public int SponsorTypeId { get; set; }
        public string ClientIdentiy { get; set; }
        public int StatusId { get; set; }
        public DateTime HitOn { get; set; }
    }
}