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
    
    public partial class PageItemNew
    {
        public int PageItemId { get; set; }
        public int PageId { get; set; }
        public int PageItemType { get; set; }
        public string PageHeaderText { get; set; }
        public string PageSubHeaderText { get; set; }
        public string PageItemImage { get; set; }
        public string PageItemDetailText { get; set; }
        public string PageItemSubDetail { get; set; }
        public int StatusId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ActivatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public byte[] Version { get; set; }
        public Nullable<long> ItemOrder { get; set; }
    }
}