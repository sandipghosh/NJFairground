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
    
    public partial class PageBanner
    {
        public int PageBannerId { get; set; }
        public int BannerId { get; set; }
        public int PageId { get; set; }
        public int PageItemId { get; set; }
        public int StatusId { get; set; }
    
        public virtual Banner Banner { get; set; }
        public virtual Page Page { get; set; }
        public virtual PageItem PageItem { get; set; }
    }
}
