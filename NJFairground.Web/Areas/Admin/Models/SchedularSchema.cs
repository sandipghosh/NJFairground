using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NJFairground.Web.Areas.Admin.Models
{
    public class SchedularSchema
    {
        public int id { get; set; }
        public int pageid { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int statusid { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
    }
}