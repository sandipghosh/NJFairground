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
    
    public partial class Page
    {
        public Page()
        {
            this.Events = new HashSet<Event>();
            this.FavoritePages = new HashSet<FavoritePage>();
            this.PageBanners = new HashSet<PageBanner>();
            this.PageHeaders = new HashSet<PageHeader>();
            this.PageItems = new HashSet<PageItem>();
            this.PageItemDetails = new HashSet<PageItemDetail>();
        }
    
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageDesc { get; set; }
        public int StatusId { get; set; }
        public byte[] Version { get; set; }
        public string PageContent { get; set; }
        public string PageImage { get; set; }
    
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<FavoritePage> FavoritePages { get; set; }
        public virtual ICollection<PageBanner> PageBanners { get; set; }
        public virtual ICollection<PageHeader> PageHeaders { get; set; }
        public virtual ICollection<PageItem> PageItems { get; set; }
        public virtual ICollection<PageItemDetail> PageItemDetails { get; set; }
    }
}
