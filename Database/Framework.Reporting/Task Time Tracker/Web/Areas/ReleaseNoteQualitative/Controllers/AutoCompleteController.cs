using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using DataModel.TaskTimeTracker;

namespace ApplicationContainer.UI.Web.Areas.ReleaseNoteQualitative.Controllers
{
	public class AutoCompleteController : Controller
	{
		public ActionResult AutoCompleteSample()
		{
			return View();
		}

		public JsonResult AutoComplete()
		{
			var clientNames = new List<string>();
			//strSearch = strSearch.Trim();
			//if (SearchFor.Trim() == "name")
			//{
            var list = ClientDataManager.GetEntityDetails(ClientDataModel.Empty, SessionVariables.RequestProfile, 0);

			foreach(var item in list)
			{
				clientNames.Add(item.Name);
			}

			return Json(clientNames, JsonRequestBehavior.AllowGet);
		}

		public ActionResult MultiColumnComboBox(string SearchFor, string ControlId)
		{
			ViewBag.ProcId = SearchFor.Trim();
			ViewBag.ControlBlockId = "block" + ControlId.Trim();
			ViewBag.ControlId = ControlId.Trim();
			ViewBag.ControlTxtId = "txt" + ControlId.Trim();
			return View();
		}

		public JsonResult LoadComboData()
		{
			var clientNames = new List<string>();			
			var list = ClientDataManager.GetEntityDetails(ClientDataModel.Empty, SessionVariables.RequestProfile, 0);

			foreach (var item in list)
			{
				clientNames.Add(item.Name);
			}

			return Json(clientNames, JsonRequestBehavior.AllowGet);
		}


	}
}