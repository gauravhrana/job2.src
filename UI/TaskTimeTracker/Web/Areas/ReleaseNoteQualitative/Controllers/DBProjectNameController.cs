using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace TaskTimeTracker.UI.Web.Areas.ReleaseNoteQualitative.Controllers
{
	public class DBProjectNameController : Controller
	{

		public DBProjectNameDataModel GetById(int id)
		{
			var obj = new DBProjectNameDataModel();
			obj.DBProjectNameId = id;
			var list = DBProjectNameDataManager.GetEntityDetails(obj, SessionVariables.AuditId);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			return list[0];
		}

		//
		// GET: /ReleaseNoteBusinessDifficulty/
		public ActionResult Index(string searchString, int? page, int? pageSize)
		{
			var obj = new DBProjectNameDataModel();
			obj.Name = searchString;
			
			var list = DBProjectNameDataManager.GetEntityDetails(obj, SessionVariables.AuditId);

			if (pageSize.HasValue)
			{
				SessionVariables.DefaultRowCount = pageSize.Value;
			}

			var pageNumber = (page - 1 ?? 0);
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
		// GET: /ReleaseNoteBusinessDifficulty/Details/5

		public ActionResult Details(int id)
		{
			var obj = GetById(id);
			if (obj == null)
			{
				return HttpNotFound();
			}

			return View(obj);
		}

		//
		// GET: /ReleaseNoteBusinessDifficulty/Create

		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /ReleaseNoteBusinessDifficulty/Create

		[HttpPost]
		public ActionResult Create(DBProjectNameDataModel obj)
		{
			if (ModelState.IsValid)
			{
				DBProjectNameDataManager.Create(obj, SessionVariables.AuditId);
				return RedirectToAction("Index");
			}

			return View(obj);
		}

		//
		// GET: /ReleaseNoteBusinessDifficulty/Edit/5

		public ActionResult Edit(int id)
		{
			var obj = GetById(id);
			if (obj == null)
			{
				return HttpNotFound();
			}
			return View(obj);
		}

		//
		// POST: /ReleaseNoteBusinessDifficulty/Edit

		[HttpPost]
		public ActionResult Edit(DBProjectNameDataModel obj)
		{
			if (ModelState.IsValid)
			{
				DBProjectNameDataManager.Update(obj, SessionVariables.AuditId);
				return RedirectToAction("Index");
			}

			return View(obj);
		}
	
		//
		// GET: /ReleaseNoteBusinessDifficulty/Delete/5

		public ActionResult Delete(int id)
		{
			var obj = GetById(id);
			if (obj == null)
			{
				return HttpNotFound();
			}
			return View(obj);
		}

		//
		// POST: /ReleaseNoteBusinessDifficulty/Delete

		[HttpPost]
		public ActionResult Delete(DBProjectNameDataModel obj)
		{
			if (ModelState.IsValid)
			{
				DBProjectNameDataManager.Delete(obj, SessionVariables.AuditId);
				return RedirectToAction("Index");
			}

			return View(obj);
		}

	}
}
