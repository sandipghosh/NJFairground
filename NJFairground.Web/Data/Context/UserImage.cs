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
    
    public partial class UserImage
    {
        public UserImage()
        {
            this.FavoriteImages = new HashSet<FavoriteImage>();
        }
    
        public int UserImageId { get; set; }
        public int UserKey { get; set; }
        public string UserImageUrl { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual ICollection<FavoriteImage> FavoriteImages { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}