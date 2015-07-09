
namespace NJFairground.Web.Data.Implementation
{
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Data.Implementation.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Data.Interface.Base;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Linq;
    using System.Data.SqlClient;

    public class PageItemDataRepository
        : DataRepository<PageItem, PageItemModel>, IPageItemDataRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageDataRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PageItemDataRepository(UnitOfWork<NJFairgroundDBEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

        private SqlParameter InitParam<T>(string paramName, T input)
        {
            SqlParameter parameter;
            if (input == null)
                parameter = new SqlParameter(paramName, DBNull.Value);
            else
                parameter = new SqlParameter(paramName, input);

            return parameter;
        }

        /// <summary>
        /// Inserts the custom.
        /// </summary>
        /// <param name="pageItem">The page item.</param>
        /// <returns></returns>
        public PageItemModel InsertCustom(PageItemModel pageItem)
        {
            PageItemModel result = new PageItemModel();
            try
            {
                IQueryDataRepository query = new QueryDataRepository<NJFairgroundDBEntities>();
                SqlParameter[] param = new SqlParameter[]
                {
                    InitParam("@PageId", pageItem.PageId),
                    InitParam("@PageItemType", 100),
                    InitParam("@PageHeaderText", pageItem.PageHeaderText),
                    InitParam("@PageSubHeaderText", pageItem.PageSubHeaderText),
                    InitParam("@PageItemImage", pageItem.PageItemImage),
                    InitParam("@PageItemDetailText", pageItem.PageItemDetailText),
                    InitParam( "@PageItemSubDetail",pageItem.PageItemSubDetail),
                    //new SqlParameter("@PageItemSubDetail", string.IsNullOrEmpty(pageItem.PageItemSubDetail)
                    //    ?DBNull.Value:pageItem.PageItemSubDetail),
                    InitParam("@StatusId", pageItem.StatusId),
                    InitParam("@CreatedOn", pageItem.CreatedOn),
                    InitParam("@ActivatedOn", pageItem.ActivatedOn),
                    InitParam("@ItemOrder", pageItem.ItemOrder)
                };
                result = query.ExecuteQuery<PageItemModel>("EXEC InsertPageItem @PageId, @PageItemType, @PageHeaderText, @PageSubHeaderText, @PageItemImage, @PageItemDetailText, @PageItemSubDetail, @StatusId, @CreatedOn, @ActivatedOn, @ItemOrder", param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(pageItem);
            }
            return result;
        }
    }
}