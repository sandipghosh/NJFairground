﻿
namespace NJFairground.Web.Data.Implementation
{
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Data.Implementation.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;

    public class BannerDataRepository
        : DataRepository<Banner, BannerModel>, IBannerDataRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageDataRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public BannerDataRepository(UnitOfWork<NJFairgroundDBEntities> unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}