
namespace NJFairground.Web.Areas.Admin.Controllers
{
    using Newtonsoft.Json;
    using NJFairground.Web.Areas.Admin.Models.Base;
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;


    public class ContentManagerController : BaseController
    {
        private readonly IPageDataRepository _pageDataRepository;
        private readonly IPageItemDataRepository _pageItemDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManagerController"/> class.
        /// </summary>
        /// <param name="pageDataRepository">The page data repository.</param>
        /// <param name="pageItemDataRepository">The page item data repository.</param>
        public ContentManagerController(IPageDataRepository pageDataRepository,
            IPageItemDataRepository pageItemDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
            this._pageItemDataRepository = pageItemDataRepository;
        }

        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult GetPageItems()
        {
            try
            {
                //GridSearchDataModel requestSearchData = new GridSearchDataModel();
                //requestSearchData.SetPropertiesFromContext<GridSearchDataModel>(System.Web.HttpContext.Current);

                //if (requestSearchData != null)
                //{
                //    List<PageItemModel> pageItems = new List<PageItemModel>();
                //    int totalRecords = 0;

                //    if (requestSearchData._search)
                //    {
                //        string criteria = requestSearchData.filters.ToString();
                //        var searchCriteria = CommonUtility.GetLamdaExpressionFromFilter<PageItemModel>(criteria);
                //        totalRecords = this._pageItemDataRepository.GetCount(searchCriteria);

                //        users = this._pageItemDataRepository
                //            .GetList(requestSearchData.page, requestSearchData.rows, searchCriteria,
                //            x => x.CreatedOn, false).ToList();
                //    }
                //    else
                //    {
                //        totalRecords = this._pageItemDataRepository.GetCount(x => x.StatusId.Equals((int)StatusEnum.Active));
                //        pageItems = this._pageItemDataRepository
                //            .GetList(requestSearchData.page, requestSearchData.rows, x => x.StatusId.Equals((int)StatusEnum.Active),
                //            x => x.CreatedOn, false).ToList();
                //    }

                //    return new JSONActionResult(new GridDataModel()
                //    {
                //        currpage = requestSearchData.page,
                //        totalpages = (int)Math.Ceiling((float)totalRecords / (float)requestSearchData.rows),
                //        totalrecords = totalRecords,
                //        invdata = pageItems.Select(x => new
                //        {
                //            x.PageItemId,
                //            x.PageId,
                //            x.PageHeaderText,
                //            x.PageSubHeaderText,
                //            x.EmailId,
                //            x.PrimaryContact,
                //            x.Address1,
                //            x.Address2,
                //            x.State,
                //            x.City,
                //            x.Pin,
                //            x.Country,
                //            Status = ((StatusEnum)x.StatusId).ToString()
                //        })
                //    });
                //}
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return RedirectToAction("Index", "Error", new { area = "" });
        }


        #region Private members
        private string GetCustomerModelSchema()
        {
            string strSchema = string.Empty;
            GridModelSchema gridModelSchema = null;
            try
            {
                List<colModel> columnModel = new List<colModel>();
                var status = Enum.GetValues(typeof(StatusEnum)).OfType<StatusEnum>()
                    .Select(x => new { Key = (int)x, Value = x.ToString() });

                columnModel.Add(new colModel()
                {
                    name = CommonUtility.GetDisplayName((PageItemModel x) => x.PageItemImage),
                    editable = true,
                    hidden = true,
                    edittype = Edittype.custom.ToString(),
                });
                columnModel.Add(new colModel()
                {
                    name = CommonUtility.GetDisplayName((PageItemModel x) => x.PageId),
                    editable = true,
                    hidden = true,
                    edittype = Edittype.custom.ToString(),
                });
                columnModel.Add(new colModel()
                {
                    name = CommonUtility.GetDisplayName((PageItemModel x) => x.PageHeaderText),
                    editable = false,
                    searchoptions = new SearchOptions
                    {
                        sopt = new string[] 
                        {
                            SearchOperator.eq.ToString(), 
                            SearchOperator.ne.ToString(),
                            SearchOperator.bw.ToString(),
                            SearchOperator.bn.ToString(),
                            SearchOperator.cn.ToString(),
                            SearchOperator.nc.ToString(),
                            SearchOperator.ew.ToString(),
                            SearchOperator.en.ToString(),
                        }
                    }
                });
                columnModel.Add(new colModel()
                {
                    name = CommonUtility.GetDisplayName((PageItemModel x) => x.PageSubHeaderText),
                    width = 50,
                    editable = false,
                    searchoptions = new SearchOptions
                    {
                        sopt = new string[] 
                        {
                            SearchOperator.eq.ToString(), 
                            SearchOperator.ne.ToString(),
                            SearchOperator.bw.ToString(),
                            SearchOperator.bn.ToString(),
                            SearchOperator.cn.ToString(),
                            SearchOperator.nc.ToString(),
                            SearchOperator.ew.ToString(),
                            SearchOperator.en.ToString(),
                        }
                    }
                });
                columnModel.Add(new colModel()
                {
                    name = CommonUtility.GetDisplayName((PageItemModel x) => x.PageItemDetailText),
                    width = 50,
                    editable = false,
                    searchoptions = new SearchOptions
                    {
                        sopt = new string[] 
                        {
                            SearchOperator.eq.ToString(), 
                            SearchOperator.ne.ToString(),
                            SearchOperator.bw.ToString(),
                            SearchOperator.bn.ToString(),
                            SearchOperator.cn.ToString(),
                            SearchOperator.nc.ToString(),
                            SearchOperator.ew.ToString(),
                            SearchOperator.en.ToString(),
                        }
                    }
                });
                columnModel.Add(new colModel()
                {
                    name = CommonUtility.GetDisplayName((PageItemModel x) => x.StatusId),
                    editable = true,
                    hidden = true,
                    edittype = Edittype.custom.ToString(),
                });
                columnModel.Add(new colModel()
                {
                    name = "Status",
                    editable = true,
                    width = 50,
                    targetFieldName = CommonUtility.GetDisplayName((PageItemModel x) => x.StatusId),
                    edittype = Edittype.select.ToString(),
                    align = Align.center.ToString(),
                    editrules = new editrules { required = true },
                    editoptions = new editoptions { value = CommonUtility.GetGridDropdownData(status, "Key", "Value") },
                    searchoptions = new SearchOptions
                    {
                        sopt = new string[] 
                        {
                            SearchOperator.eq.ToString(), 
                            SearchOperator.ne.ToString(),
                            SearchOperator.bw.ToString(),
                            SearchOperator.bn.ToString(),
                            SearchOperator.cn.ToString(),
                            SearchOperator.nc.ToString(),
                            SearchOperator.ew.ToString(),
                            SearchOperator.en.ToString(),
                        }
                    }
                });
                columnModel.Add(new colModel()
                {
                    name = "Actions",
                    index = "act",
                    width = 30,
                    sortable = false,
                    search = false
                });

                gridModelSchema = new GridModelSchema(columnModel);
                return JsonConvert.SerializeObject(gridModelSchema, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                }).ToBase64Encode();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return strSchema;
        }
        #endregion

    }
}
