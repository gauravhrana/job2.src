﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebAPISample.Controllers
{
	public class CustomController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult MultiColumnComboBox(string SearchFor, string ControlId)
		{
			ViewBag.ProcId = SearchFor.Trim();
			ViewBag.ControlBlockId = "block" + ControlId.Trim();
			ViewBag.ControlId = ControlId.Trim();
			ViewBag.ControlTxtId = "txt" + ControlId.Trim();
			return View();
		}		

		public JsonResult LoadComboData(string strSearch, string SearchFor)
		{
			var clientNames = new List<string>();
			strSearch = strSearch.Trim();
			if (SearchFor.Trim() == "name")
			{
				var list = TaskTimeTracker.Components.BusinessLayer.ClientDataManager.GetList(SessionVariables.RequestProfile);

				foreach(var item in list)
				{
					clientNames.Add(item.Name);
				}
			}
			return Json(clientNames, JsonRequestBehavior.AllowGet);
		}
 
 
	}
}