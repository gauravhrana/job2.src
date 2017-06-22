using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Areas.ApplicationDevelopment.Controllers
{
	public class CustomTimeCategoryController : Controller
	{

		public CustomTimeCategoryDataModel GetById(int id)
		{
			var obj = new CustomTimeCategoryDataModel();
			obj.CustomTimeCategoryId = id;
			var list = CustomTimeCategoryDataManager.GetEntityDetails(obj, SessionVariables.RequestProfile);
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
			var obj = new CustomTimeCategoryDataModel();
			obj.Name = searchString;
			var list = CustomTimeCategoryDataManager.GetEntityDetails(obj, SessionVariables.RequestProfile);

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
		public ActionResult Create(CustomTimeCategoryDataModel obj)
		{
			if (ModelState.IsValid)
			{
				CustomTimeCategoryDataManager.Create(obj, SessionVariables.RequestProfile);
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
		public ActionResult Edit(CustomTimeCategoryDataModel obj)
		{
			if (ModelState.IsValid)
			{
				CustomTimeCategoryDataManager.Update(obj, SessionVariables.RequestProfile);
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
		public ActionResult Delete(CustomTimeCategoryDataModel obj)
		{
			if (ModelState.IsValid)
			{
				CustomTimeCategoryDataManager.Delete(obj, SessionVariables.RequestProfile);
				return RedirectToAction("Index");
			}

			return View(obj);
		}

	}
}