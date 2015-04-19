﻿

namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class FavoritePageRequestDto : RequestBase
    {
        public UserInfoModel UserInfo { get; set; }
        public int PageItemId { get; set; }
    }
}