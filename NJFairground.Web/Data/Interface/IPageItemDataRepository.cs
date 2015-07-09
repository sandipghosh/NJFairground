
namespace NJFairground.Web.Data.Interface
{
    using NJFairground.Web.Data.Interface.Base;
    using NJFairground.Web.Models;

    public interface IPageItemDataRepository : IRepository<PageItemModel>
    {
        PageItemModel InsertCustom(PageItemModel pageItem);
    }
}
