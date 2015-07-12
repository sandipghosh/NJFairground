
namespace NJFairground.Web.Data.Implementation
{
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Data.Implementation.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;

    public class HitCounterDetailDataRepository
        : DataRepository<HitCounterDetail, HitCounterDetailViewModel>, IHitCounterDetailDataRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HitCounterDetailDataRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public HitCounterDetailDataRepository(UnitOfWork<NJFairgroundDBEntities> unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}