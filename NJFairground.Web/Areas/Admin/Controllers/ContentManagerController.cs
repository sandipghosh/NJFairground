
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
