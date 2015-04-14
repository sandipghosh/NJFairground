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
    
    public partial class UserInfo
    {
        public UserInfo()
        {
            this.FavoritePages = new HashSet<FavoritePage>();
            this.UserImages = new HashSet<UserImage>();
        }
    
        public int UserKey { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StatusId { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual ICollection<FavoritePage> FavoritePages { get; set; }
        public virtual ICollection<UserImage> UserImages { get; set; }
    }
}
