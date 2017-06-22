using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.Areas.ApplicationDevelopment.Controllers
{
	public class DBComponentNameController : Controller
	{

		public DBComponentNameDataModel GetById(int id)
		{
			var obj = new DBComponentNameDataModel();
			obj.DBComponentNameId = id;
			var list = DBComponentNameDataManager.GetEntityDetails(obj, SessionVariables.RequestProfile);
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
			var obj = new DBComponentNameDataModel();
			obj.Name = searchString;
			var list = DBComponentNameDataManager.GetEntitySearch(obj, SessionVariables.RequestProfile);

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
		public ActionResult Create(DBComponentNameDataModel obj)
		{
			if (ModelState.IsValid)
			{
				DBComponentNameDataManager.Create(obj, SessionVariables.RequestProfile);
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
		public ActionResult Edit(DBComponentNameDataModel obj)
		{
			if (ModelState.IsValid)
			{
				DBComponentNameDataManager.Update(obj, SessionVariables.RequestProfile);
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
		public ActionResult Delete(DBComponentNameDataModel obj)
		{
			if (ModelState.IsValid)
			{
				DBComponentNameDataManager.Delete(obj, SessionVariables.RequestProfile);
				return RedirectToAction("Index");
			}

			return View(obj);
		}

	}
}
