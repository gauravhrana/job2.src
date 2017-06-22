using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;

namespace ApplicationContainer.UI.Web.Areas.FunctionalityDevelopmentStep.Controllers
{
    public class FunctionalityXFunctionalityDevelopmentStepController : Controller
    {
		string settingCategory = "FunctionalityXFunctionalityDevelopmentStepIndexView";

		#region private methods

		private List<string> GetKendoColumnSetByFCMode(int fcModeId)
		{
			var lstColumns = new List<string>();

			var systemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityDevelopmentStep;

			var dtConfigurations = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, fcModeId, string.Empty);

			foreach (DataRow row in dtConfigurations.Rows)
			{
				var columnName = Convert.ToString(row[DataModel.Framework.Configuration.FieldConfigurationDataModel.DataColumns.Name]);
				//var title = Convert.ToString(row[DataModel.Framework.Configuration.FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName]);
				//var width = Convert.ToString(row[DataModel.Framework.Configuration.FieldConfigurationDataModel.DataColumns.Width]);

				//str.AppendLine("        { field: '" + columnName + "', title: '" + title + "', width: " + width + " },");

				lstColumns.Add(columnName);
			}


			return lstColumns;
		}

		private string GetAllKendoColumns()
		{
			var str = new StringBuilder();

			str.AppendLine("    columns: [");
			str.AppendLine("        {");
			str.AppendLine("            title: \"\", field: 'FunctionalityXFunctionalityDevelopmentStepId',");
			str.AppendLine("            filterable: false, sortable: false, width:30,");
			str.AppendLine("            headerTemplate: \"<input type='checkbox' id='checkAll'/>\",");
			str.AppendLine("            template: \"<input onclick='CheckWithHeader();' class='check-box' id='chkBox${FunctionalityXFunctionalityDevelopmentStepId}' type='checkbox' value='${FunctionalityXFunctionalityDevelopmentStepId}' /> \"");
			str.AppendLine("        },");
			str.AppendLine("        { field: \"Functionality\", title: \"Functionality\", width: 140, visible: false },");
			str.AppendLine("        { field: \"FunctionalityDevelopmentStep\", title: \"FunctionalityDevelopmentStep\", width: 190, visible: false },");
			str.AppendLine("        { field: \"SortOrder\", title: \"Sort Order\", width: 100, visible: false },");
			str.AppendLine("        { field: \"Version\", title: \"Version\", width: 100, visible: false },");
			str.AppendLine("        { field: \"LastAction\", title: \"Last Action\", width: 140, visible: false },");			

			// Add Button Columns
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", width: 140, field: 'FunctionalityXFunctionalityDevelopmentStepId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/FunctionalityDevelopmentStep/FunctionalityXFunctionalityDevelopmentStep/Details/${FunctionalityXFunctionalityDevelopmentStepId}'>Details</a>\"");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", width: 140, field: 'FunctionalityXFunctionalityDevelopmentStepId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/FunctionalityDevelopmentStep/FunctionalityXFunctionalityDevelopmentStep/Edit/${FunctionalityXFunctionalityDevelopmentStepId}'>Edit</a>\"");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", width: 140, field: 'FunctionalityXFunctionalityDevelopmentStepId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/FunctionalityDevelopmentStep/FunctionalityXFunctionalityDevelopmentStep/Delete/${FunctionalityXFunctionalityDevelopmentStepId}'>Delete</a>\"");
			str.AppendLine("        }");
			str.AppendLine("    ]");

			return str.ToString();
		}

		private string GetKendoGridConfiguration(string searchString, int fcModeId)
		{
			var str = new StringBuilder();

			str.AppendLine("");

			//str.AppendLine("$(\"#grid\").kendoGrid({");
			str.AppendLine("    " + GetAllKendoColumns() + ",");
			str.AppendLine("    dataSource: {");
			str.AppendLine("        transport: {");
			str.AppendLine("            read: \"/FunctionalityDevelopmentStep/FunctionalityXFunctionalityDevelopmentStep/IndexResult?searchString=" + searchString + "\"");
			str.AppendLine("        },");
			str.AppendLine("        schema: {");
			str.AppendLine("            model: {");
			str.AppendLine("                    fields: {");
			str.AppendLine("                        FunctionalityXFunctionalityDevelopmentStepId: { type: \"number\" },");
			str.AppendLine("                        FunctionalityDevelopmentStep: { type: \"string\" },");
			str.AppendLine("                        Functionality: { type: \"string\" },");
			str.AppendLine("                        SortOrder: { type: \"number\" },");
			str.AppendLine("                        Version: { type: \"string\" },");			
			str.AppendLine("                        LastAction: { type: \"string\" },");			
			str.AppendLine("                    }");
			str.AppendLine("            }");
			str.AppendLine("        },");
			str.AppendLine("        pageSize: 10");
			str.AppendLine("    },");
			str.AppendLine("    groupable: true,");
			str.AppendLine("    sortable: true,");
			//str.AppendLine("    filterable: true,");
			str.AppendLine("    pageable: {");
			str.AppendLine("        refresh: true,");
			str.AppendLine("        pageSizes: true,");
			str.AppendLine("        buttonCount: 5");
			str.AppendLine("    }");
			//str.AppendLine("});");

			return str.ToString();
		}

		#endregion

		#region Public Methods

		public FunctionalityXFunctionalityDevelopmentStepDataModel GetById(int id)
		{
			var obj = new FunctionalityXFunctionalityDevelopmentStepDataModel();
			obj.FunctionalityXFunctionalityDevelopmentStepId = id;
			var list = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityDevelopmentStepDataManager.GetEntityDetails(obj, SessionVariables.RequestProfile);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			return list[0];
		}

		/// <summary>
		/// This method will be called by AJAX from Index Page for generating column set according to the passed fcModeId
		/// </summary>
		/// <param name="searchString"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public JsonResult ReloadKendoGridConfiguration(string fcModeId, string searchString)
		{
			PerferenceUtility.UpdateUserPreference(settingCategory, ApplicationCommon.FieldConfigurationMode, fcModeId.ToString());

			var configString = GetKendoColumnSetByFCMode(Convert.ToInt32(fcModeId));
			return Json(configString, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// This method will be called by AJAX from Index Page for generating Kendo Grid
		/// </summary>
		/// <param name="searchString"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public JsonResult IndexResult(int? functionalityId, int? page, int? pageSize)
		{
			var obj = new FunctionalityXFunctionalityDevelopmentStepDataModel();
			obj.FunctionalityId = functionalityId;
			var list = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityDevelopmentStepDataManager.GetEntitySearch(obj, SessionVariables.RequestProfile);
			return Json(list, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Currently not in use, but this method can be called to retrieve the javascript code, will need further time to implement
		/// </summary>
		/// <param name="searchString"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public JavaScriptResult IndexGridConfiguration(string searchString, int? page, int? pageSize)
		{
			var str = new StringBuilder();

			str.AppendLine("$(\"#grid\").kendoGrid({");
			str.AppendLine("    columns: [");
			str.AppendLine("        { field: \"FunctionalityXFunctionalityDevelopmentStepId\", title: \"FunctionalityXFunctionalityDevelopmentStepId\", width: 140 },");
			str.AppendLine("        { field: \"Functionality\", title: \"Functionality\", width: 140 },");
			str.AppendLine("        { field: \"FunctionalityDevelopmentStep\", title: \"FunctionalityDevelopmentStep\", width: 190 },");
			str.AppendLine("        { field: \"SortOrder\", title: \"SortOrder\", width: 190 },");
			str.AppendLine("        { field: \"Version\", title: \"Version\", width: 190 },");
			//str.AppendLine("        {");
			//str.AppendLine("            field: \"DateCreated\", title: \"Date Created\",");
			//str.AppendLine("            format: \"{0:dd-MMM-yyyy}\",");
			//str.AppendLine("            parseFormats: [\"yyyy-MM-dd'T'HH:mm:ss.zz\"],");
			//str.AppendLine("            filterable: false");
			//str.AppendLine("        },");
			//str.AppendLine("        {");
			//str.AppendLine("            field: \"DateModified\", title: \"Date Modified\",");
			//str.AppendLine("            format: \"{0:dd-MMM-yyyy}\",");
			//str.AppendLine("            parseFormats: [\"yyyy-MM-dd'T'HH:mm:ss.zz\"],");
			//str.AppendLine("            filterable: false");
			//str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", field: 'FunctionalityXFunctionalityDevelopmentStepId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/FunctionalityDevelopmentStep/FunctionalityXFunctionalityDevelopmentStep/Details/${FunctionalityXFunctionalityDevelopmentStepId}'>Details</a>\"");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", field: 'FunctionalityXFunctionalityDevelopmentStepId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/FunctionalityDevelopmentStep/FunctionalityXFunctionalityDevelopmentStep/Edit/${FunctionalityXFunctionalityDevelopmentStepId}'>Edit</a>\"");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", field: 'FunctionalityXFunctionalityDevelopmentStepId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/FunctionalityDevelopmentStep/FunctionalityXFunctionalityDevelopmentStep/Delete/${FunctionalityXFunctionalityDevelopmentStepId}'>Delete</a>\"");
			str.AppendLine("        }");
			str.AppendLine("    ],");
			str.AppendLine("    dataSource: {");
			str.AppendLine("        transport: {");
			str.AppendLine("            read: \"/FunctionalityDevelopmentStep/FunctionalityXFunctionalityDevelopmentStep/IndexResult\"");
			str.AppendLine("        },");
			str.AppendLine("        schema: {");
			str.AppendLine("            model: {");
			str.AppendLine("                    fields: {");
			str.AppendLine("                        FunctionalityXFunctionalityDevelopmentStep: { type: \"number\" },");
			str.AppendLine("                        Functionality: { type: \"string\" },");
			str.AppendLine("                        FunctionalityDevelopmentStep: { type: \"string\" },");
			str.AppendLine("                        SortOrder: { type: \"number\" }");
			str.AppendLine("                        Version: { type: \"string\" }");
			str.AppendLine("                    }");
			str.AppendLine("            }");
			str.AppendLine("        },");
			str.AppendLine("        pageSize: 10");
			str.AppendLine("    },");
			str.AppendLine("    groupable: true,");
			str.AppendLine("    sortable: true,");
			str.AppendLine("    filterable: true,");
			str.AppendLine("    pageable: {");
			str.AppendLine("        refresh: true,");
			str.AppendLine("        pageSizes: true,");
			str.AppendLine("        buttonCount: 5");
			str.AppendLine("    }");
			str.AppendLine("});");


			var result = new JavaScriptResult();
			result.Script = str.ToString();
			return result;
		}

		public JsonResult GetSearchFilterColumns()
		{
			var data = new FieldConfigurationDataModel();
			data.SystemEntityTypeId = SystemEntity.FunctionalityXFunctionalityDevelopmentStep.Value();
			data.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;

            var list = FieldConfigurationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfo);

			return Json(list, JsonRequestBehavior.AllowGet);
		}

		public int GenerateSuperKey(string Ids)
		{
			int superKeyId = 0;

			if (!string.IsNullOrEmpty(Ids))
			{
				var ids = Ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

				var systemEntityTypeId = (int)SystemEntity.FunctionalityXFunctionalityDevelopmentStep;

				superKeyId = ApplicationCommon.GenerateSuperKey(UIHelper.ArrayToCollection(ids), systemEntityTypeId);
			}

			return superKeyId;
		}


		//
		// GET: /FunctionalityDevelopmentStep/

		public ActionResult Index(string searchString, int? page, int? pageSize)
		{
			//SetViewBagSearchFunctionality();

			var dtFCModes = FieldConfigurationUtility.GetApplicableModesList(Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityDevelopmentStep);

			var firstFCMode = PerferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.FieldConfigurationMode, settingCategory);

			var listItemsfieldConfigurationMode = MvcCommon.GetListItems(dtFCModes,
				DataModel.Framework.Configuration.FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId,
				StandardDataModel.StandardDataColumns.Name,
				firstFCMode);

			ViewBag.FieldConfigurationMode = new SelectList(listItemsfieldConfigurationMode, "Value", "Text", firstFCMode);

			ViewBag.KendoUIConfigurationString = GetKendoGridConfiguration(searchString, firstFCMode);

			return View();
		}

		public ActionResult Index2(int searchString, int? page, int? pageSize)
		{
			var obj = new FunctionalityXFunctionalityDevelopmentStepDataModel();
			obj.FunctionalityId = searchString;
			var list = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityDevelopmentStepDataManager.GetEntitySearch(obj, SessionVariables.RequestProfile);

			if (pageSize.HasValue)
			{
				SessionVariables.DefaultRowCount = pageSize.Value;
			}

			int pageNumber = (page - 1 ?? 0);
			ViewBag.searchString = searchString;

			ViewBag.CurrentPage = list.Count > 0 ? pageNumber + 1 : 0;
			ViewBag.TotalPages = list.Count / SessionVariables.DefaultRowCount;
			if (list.Count % SessionVariables.DefaultRowCount != 0)
			{
				ViewBag.TotalPages = (list.Count / SessionVariables.DefaultRowCount) + 1;
			}

			list = list.Count > 0 ? list.Skip(pageNumber * SessionVariables.DefaultRowCount).Take(SessionVariables.DefaultRowCount).ToList() : list;
			return View(list);
		}

		//
		// GET: /FunctionalityDevelopmentStep/Details/5
		
		public ActionResult Details(int id, bool? isMultiple)
		{
			var lstItems = new List<FunctionalityXFunctionalityDevelopmentStepDataModel>();
			if (isMultiple != null && isMultiple.Value)
			{
				var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(SystemEntity.FunctionalityXFunctionalityDevelopmentStep.Value(), id.ToString());
				foreach (var itemId in lstEntityKeys)
				{
					lstItems.Add(GetById(itemId));
				}
			}
			else
			{
				var obj = GetById(id);
				if (obj == null)
				{
					return HttpNotFound();
				}
				lstItems.Add(obj);

			}
			return View(lstItems);
		}

		public ActionResult Details2(int id)
		{
			var obj = GetById(id);
			if (obj == null)
			{
				return HttpNotFound();
			}
			return View(obj);
		}

		//
		// GET: /FunctionalityDevelopmentStep/Create

		public ActionResult Create()
		{
			SetViewBagFunctionality();
			return View();
		}

		[HttpPost]
		public JsonResult getFunctionalityJson()
		{
			return Json(SetViewBagSearchFunctionality(),JsonRequestBehavior.AllowGet);
		}

		private List<SelectListItem> SetViewBagSearchFunctionality()
		{
			var dtItems1 = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
			var items1 = MvcCommon.GetListItems(dtItems1,
				FunctionalityDataModel.DataColumns.FunctionalityId,
				StandardDataModel.StandardDataColumns.Name,
				10006);
			return items1;
			//ViewBag.FunctionalityId = items1;
		}

		private void SetViewBagFunctionality()
		{
			var dtItems1 = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
			var items1 = MvcCommon.GetListItems(dtItems1,
				FunctionalityDataModel.DataColumns.FunctionalityId,
				StandardDataModel.StandardDataColumns.Name,
				10006);
			ViewBag.FunctionalityId = items1;

			var dtItems2 = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDevelopmentStepDataManager.GetList(SessionVariables.RequestProfile);
			var items2 = MvcCommon.GetListItems(dtItems2,
				FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId,
				StandardDataModel.StandardDataColumns.Name,
				10006);
			ViewBag.FunctionalityDevelopmentStepId = items2;
		}

		private void SetViewBagFunctionality(int? functionalityId, int? functionalityDevelopmentStepId)
		{
			var dtItems1 = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
			var items1 = MvcCommon.GetListItems(dtItems1,
				FunctionalityDataModel.DataColumns.FunctionalityId,
				StandardDataModel.StandardDataColumns.Name,
				functionalityId);
			ViewBag.FunctionalityId = items1;

			var dtItems2 = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDevelopmentStepDataManager.GetList(SessionVariables.RequestProfile);
			var items2 = MvcCommon.GetListItems(dtItems2,
				FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId,
				StandardDataModel.StandardDataColumns.Name,
				functionalityDevelopmentStepId);
			ViewBag.FunctionalityDevelopmentStepId = items2;
		}
		//
		// POST: /FunctionalityDevelopmentStep/Create

		[HttpPost]
		public ActionResult Create(FunctionalityXFunctionalityDevelopmentStepDataModel obj)
		{
			if (ModelState.IsValid)
			{
				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityDevelopmentStepDataManager.Create(obj, SessionVariables.RequestProfile);
				return RedirectToAction("Index");
			}

			return View(obj);
		}

		//
		// GET: /FunctionalityDevelopmentStep/Edit/5

		public ActionResult Edit2(int id)
		{
			var obj = GetById(id);
			if (obj == null)
			{
				return HttpNotFound();
			}
			return View(obj);
		}

		public ActionResult Edit(int id, bool? isMultiple)
		{
			var lstItems = new List<FunctionalityXFunctionalityDevelopmentStepDataModel>();
			if (isMultiple != null && isMultiple.Value)
			{
				var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(SystemEntity.FunctionalityXFunctionalityDevelopmentStep.Value(), id.ToString());
				foreach (var itemId in lstEntityKeys)
				{
					var obj = GetById(itemId);
					lstItems.Add(GetById(itemId));
					SetViewBagFunctionality(obj.FunctionalityId,obj.FunctionalityDevelopmentStepId);
				}
			}
			else
			{
				var obj = GetById(id);
				if (obj == null)
				{
					return HttpNotFound();
				}
				lstItems.Add(obj);
				SetViewBagFunctionality(obj.FunctionalityId, obj.FunctionalityDevelopmentStepId);

			}
			return View(lstItems);
		}

		//
		// POST: /FunctionalityDevelopmentStep/Edit

		[HttpPost]
		public ActionResult Edit(List<FunctionalityXFunctionalityDevelopmentStepDataModel> items)
		{
			if (ModelState.IsValid)
			{
				foreach (var item in items)
				{
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityDevelopmentStepDataManager.Update(item, SessionVariables.RequestProfile);
				}
				return RedirectToAction("Index");
			}

			return View(items);
		}

		//
		// GET: /FunctionalityDevelopmentStep/Delete/5

		//public ActionResult Delete(int id)
		//{
		//	var obj = GetById(id);
		//	if (obj == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(obj);
		//}

		public ActionResult Delete(int id, bool? isMultiple)
		{
			var lstItems = new List<FunctionalityXFunctionalityDevelopmentStepDataModel>();
			if (isMultiple != null && isMultiple.Value)
			{
				var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(SystemEntity.FunctionalityXFunctionalityDevelopmentStep.Value(), id.ToString());
				foreach (var itemId in lstEntityKeys)
				{
					lstItems.Add(GetById(itemId));
				}
			}
			else
			{
				var obj = GetById(id);
				if (obj == null)
				{
					return HttpNotFound();
				}
				lstItems.Add(obj);

			}
			return View(lstItems);
		}

		//
		// POST: /FunctionalityDevelopmentStep/Delete

		[HttpPost]
		public ActionResult Delete(List<FunctionalityXFunctionalityDevelopmentStepDataModel> items)
		{
			if (ModelState.IsValid)
			{
				foreach (var item in items)
				{
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityDevelopmentStepDataManager.Delete(item, SessionVariables.RequestProfile);
				}
				return RedirectToAction("Index");
			}

			return View(items);
		}

		#endregion
    }
}
