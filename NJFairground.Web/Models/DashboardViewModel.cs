﻿
using System.Collections.Generic;
namespace NJFairground.Web.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.NotificationLogDetail = new List<NotificationLogViewModel>();
        }
        public int TotalBannerHits { get; set; }
        public int TotalSplashHits { get; set; }
        public int TotalActiveUsers { get; set; }
        public int TotalActiveiOSUsers { get; set; }
        public int TotalActiveAndroidUsers { get; set; }
        public int TotalActiveGallaryUsage { get; set; }

        public IList<NotificationLogViewModel> NotificationLogDetail { get; set; }
    }
}