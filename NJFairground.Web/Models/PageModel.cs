
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class PageModel:BaseModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageDesc { get; set; }
        public int StatusId { get; set; }
    }
}