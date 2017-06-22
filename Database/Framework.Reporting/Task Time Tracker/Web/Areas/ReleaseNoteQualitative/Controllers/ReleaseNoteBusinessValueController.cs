using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Framework.Components.ReleaseLog;
using Framework.Components.ReleaseLog.DomainModel;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Areas.ReleaseNoteQualitative.Controllers
{
	public class ReleaseNoteBusinessValueController : Controller
	{

		string settingCategory = "ReleaseNoteBusinessValueIndexView";

		#region private methods

		private List<string> GetKendoColumnSetByFCMode(int fcModeId)
		{
			var lstColumns = new List<string>();

			var systemEntityTypeId = (int)SystemEntity.ReleaseNoteBusinessValue;

			var dtConfigurations = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, fcModeId, string.Empty);

			foreach (DataRow row in dtConfigurations.Rows)
			{
				var columnName = Convert.ToString(row[FieldConfigurationDataModel.DataColumns.Name]);
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
			str.AppendLine("            title: \" \", field: 'ReleaseNoteBusinessValueId',");
			str.AppendLine("            filterable: false, sortable: false, width:30,");
			str.AppendLine("            headerTemplate: \"<input type='checkbox' id='checkAll'/>\",");
			str.AppendLine("            template: \"<input onclick='CheckWithHeader();' class='check-box' id='chkBox${ReleaseNoteBusinessValueId}' type='checkbox' value='${ReleaseNoteBusinessValueId}' /> \"");
			str.AppendLine("        },");
			str.AppendLine("        { field: \"Name\", title: \"Name\", width: 140, visible: false },");
			str.AppendLine("        { field: \"Description\", title: \"Description\", width: 190, visible: false },");
			str.AppendLine("        { field: \"SortOrder\", title: \"Sort Order\", width: 100, visible: false },");
			str.AppendLine("        { field: \"LastAction\", title: \"Last Action\", width: 140, visible: false },");
			str.AppendLine("        { field: \"UpdatedBy\", title: \"Updated By\", width: 140, visible: false },");
			str.AppendLine("        {");
			str.AppendLine("            field: \"UpdatedDate\", width: 140, visible: false, title: \"Updated Date\",");
			str.AppendLine("            format: \"{0:dd-MMM-yyyy}\",");
			str.AppendLine("            parseFormats: [\"yyyy-MM-dd'T'HH:mm:ss.zz\"],");
			str.AppendLine("            filterable: false");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            field: \"DateCreated\", width: 140, visible: false, title: \"Date Created\",");
			str.AppendLine("            format: \"{0:dd-MMM-yyyy}\",");
			str.AppendLine("            parseFormats: [\"yyyy-MM-dd'T'HH:mm:ss.zz\"],");
			str.AppendLine("            filterable: false");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            field: \"DateModified\", width: 140, visible: false, title: \"Date Modified\",");
			str.AppendLine("            format: \"{0:dd-MMM-yyyy}\",");
			str.AppendLine("            parseFormats: [\"yyyy-MM-dd'T'HH:mm:ss.zz\"],");
			str.AppendLine("            filterable: false");
			str.AppendLine("        },");

			// Add Button Columns
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", width: 140, field: 'ReleaseNoteBusinessValueId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/ReleaseNoteQualitative/ReleaseNoteBusinessValue/Details/${ReleaseNoteBusinessValueId}'>Details</a>\"");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", width: 140, field: 'ReleaseNoteBusinessValueId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/ReleaseNoteQualitative/ReleaseNoteBusinessValue/Edit/${ReleaseNoteBusinessValueId}'>Edit</a>\"");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", width: 140, field: 'ReleaseNoteBusinessValueId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/ReleaseNoteQualitative/ReleaseNoteBusinessValue/Delete/${ReleaseNoteBusinessValueId}'>Delete</a>\"");
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
			str.AppendLine("            read: \"/ReleaseNoteQualitative/ReleaseNoteBusinessValue/IndexResult?searchString=" + searchString + "\"");
			str.AppendLine("        },");
			str.AppendLine("        schema: {");
			str.AppendLine("            model: {");
			str.AppendLine("                    fields: {");
			str.AppendLine("                        ReleaseNoteBusinessValueId: { type: \"number\" },");
			str.AppendLine("                        Name: { type: \"string\" },");
			str.AppendLine("                        Description: { type: \"string\" },");
			str.AppendLine("                        SortOrder: { type: \"number\" },");
			str.AppendLine("                        LastAction: { type: \"string\" },");
			str.AppendLine("                        UpdatedBy: { type: \"string\" },");
			str.AppendLine("                        UpdatedDate: { type: \"date\" },");
			str.AppendLine("                        DateCreated: { type: \"date\" },");
			str.AppendLine("                        DateModified: { type: \"date\" }");
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

		public ReleaseNoteBusinessValueDataModel GetById(int id)
		{
			var obj = new ReleaseNoteBusinessValueDataModel();
			obj.ReleaseNoteBusinessValueId = id;
			var list = ReleaseNoteBusinessValueDataManager.GetEntityDetails(obj, SessionVariables.RequestProfile);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			return list[0];
		}

		/// <summary>
		/// This method will be called by AJAX from Index Page for generating column set according to the passed FCModeId
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
		public JsonResult IndexResult(string name, int? page, int? pageSize)
		{
			var obj = new ReleaseNoteBusinessValueDataModel();
			obj.Name = name;
			var list = ReleaseNoteBusinessValueDataManager.GetEntitySearch(obj, SessionVariables.RequestProfile);
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
			str.AppendLine("        { field: \"Name\", title: \"Name\", width: 140 },");
			str.AppendLine("        { field: \"Description\", title: \"Description\", width: 190 },");
			str.AppendLine("        {");
			str.AppendLine("            field: \"DateCreated\", title: \"Date Created\",");
			str.AppendLine("            format: \"{0:dd-MMM-yyyy}\",");
			str.AppendLine("            parseFormats: [\"yyyy-MM-dd'T'HH:mm:ss.zz\"],");
			str.AppendLine("            filterable: false");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            field: \"DateModified\", title: \"Date Modified\",");
			str.AppendLine("            format: \"{0:dd-MMM-yyyy}\",");
			str.AppendLine("            parseFormats: [\"yyyy-MM-dd'T'HH:mm:ss.zz\"],");
			str.AppendLine("            filterable: false");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", field: 'ReleaseNoteBusinessValueId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/ReleaseNoteQualitative/ReleaseNoteBusinessValue/Details/${ReleaseNoteBusinessValueId}'>Details</a>\"");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", field: 'ReleaseNoteBusinessValueId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/ReleaseNoteQualitative/ReleaseNoteBusinessValue/Edit/${ReleaseNoteBusinessValueId}'>Edit</a>\"");
			str.AppendLine("        },");
			str.AppendLine("        {");
			str.AppendLine("            title: \" \", field: 'ReleaseNoteBusinessValueId',");
			str.AppendLine("            filterable: false,");
			str.AppendLine("            template: \"<a href='/ReleaseNoteQualitative/ReleaseNoteBusinessValue/Delete/${ReleaseNoteBusinessValueId}'>Delete</a>\"");
			str.AppendLine("        }");
			str.AppendLine("    ],");
			str.AppendLine("    dataSource: {");
			str.AppendLine("        transport: {");
			str.AppendLine("            read: \"/ReleaseNoteQualitative/ReleaseNoteBusinessValue/IndexResult\"");
			str.AppendLine("        },");
			str.AppendLine("        schema: {");
			str.AppendLine("            model: {");
			str.AppendLine("                    fields: {");
			str.AppendLine("                        Name: { type: \"string\" },");
			str.AppendLine("                        Description: { type: \"string\" },");
			str.AppendLine("                        DateCreated: { type: \"date\" },");
			str.AppendLine("                        DateModified: { type: \"date\" }");
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
			data.SystemEntityTypeId = SystemEntity.ReleaseNoteBusinessValue.Value();
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

				var systemEntityTypeId = (int)SystemEntity.ReleaseNoteBusinessValue;

				superKeyId = ApplicationCommon.GenerateSuperKey(UIHelper.ArrayToCollection(ids), systemEntityTypeId);
			}

			return superKeyId;
		}

		// GET: /ReleaseNoteBusinessValue/
		public ActionResult Index(string searchString, int? page, int? pageSize)
		{

			var dtFCModes = FieldConfigurationUtility.GetApplicableModesList(SystemEntity.ReleaseNoteBusinessValue);

			var firstFCMode = PerferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.FieldConfigurationMode, settingCategory);

			var listItemsfieldConfigurationMode = MvcCommon.GetListItems(dtFCModes,
				FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId,
				StandardDataModel.StandardDataColumns.Name,
				firstFCMode);

			ViewBag.FieldConfigurationMode = new SelectList(listItemsfieldConfigurationMode, "Value", "Text", firstFCMode);

			ViewBag.KendoUIConfigurationString = GetKendoGridConfiguration(searchString, firstFCMode);

			return View();
		}

		public ActionResult Index2(string searchString, int? page, int? pageSize)
		{
			var obj = new ReleaseNoteBusinessValueDataModel();
			obj.Name = searchString;
			var list = ReleaseNoteBusinessValueDataManager.GetEntitySearch(obj, SessionVariables.RequestProfile);

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
		// GET: /ReleaseNoteBusinessValue/Details/5
		public ActionResult Details(int id, bool? isMultiple)
		{
			var lstItems = new List<ReleaseNoteBusinessValueDataModel>();
			if (isMultiple != null && isMultiple.Value)
			{
				var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(SystemEntity.ReleaseNoteBusinessValue.Value(), id.ToString());
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
		// GET: /ReleaseNoteBusinessValue/Create

		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /ReleaseNoteBusinessValue/Create
		[HttpPost]
		public ActionResult Create(ReleaseNoteBusinessValueDataModel obj)
		{
			if (ModelState.IsValid)
			{
				ReleaseNoteBusinessValueDataManager.Create(obj, SessionVariables.RequestProfile);
				return RedirectToAction("Index");
			}

			return View(obj);
		}

		//
		// GET: /ReleaseNoteBusinessValue/Edit/5

		public ActionResult Edit(int id, bool? isMultiple)
		{
			var lstItems = new List<ReleaseNoteBusinessValueDataModel>();
			if (isMultiple != null && isMultiple.Value)
			{
				var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(SystemEntity.ReleaseNoteBusinessValue.Value(), id.ToString());
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
		// POST: /ReleaseNoteBusinessValue/Edit

		[HttpPost]
		public ActionResult Edit(List<ReleaseNoteBusinessValueDataModel> items)
		{
			if (ModelState.IsValid)
			{
				foreach (var item in items)
				{
					ReleaseNoteBusinessValueDataManager.Update(item, SessionVariables.RequestProfile);
				}
				return RedirectToAction("Index");
			}

			return View(items);
		}

		//
		// GET: /ReleaseNoteBusinessValue/Delete/5

		public ActionResult Delete(int id, bool? isMultiple)
		{
			var lstItems = new List<ReleaseNoteBusinessValueDataModel>();
			if (isMultiple != null && isMultiple.Value)
			{
				var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(SystemEntity.ReleaseNoteBusinessValue.Value(), id.ToString());
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
		// POST: /ReleaseNoteBusinessValue/Delete
		[HttpPost]
		public ActionResult Delete(List<ReleaseNoteBusinessValueDataModel> items)
		{
			if (ModelState.IsValid)
			{
				foreach (var item in items)
				{
					ReleaseNoteBusinessValueDataManager.Delete(item, SessionVariables.RequestProfile);
				}
				return RedirectToAction("Index");
			}

			return View(items);
		}

		#endregion

	}
}
