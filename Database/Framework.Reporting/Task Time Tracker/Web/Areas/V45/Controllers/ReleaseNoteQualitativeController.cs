using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Areas.V45.Controllers
{
    public class ReleaseNoteQualitativeController : Controller
    {

        public Framework.Components.ReleaseLog.DomainModel.ReleaseNoteQualitativeDataModel GetById(int id)
        {
            var obj = new Framework.Components.ReleaseLog.DomainModel.ReleaseNoteQualitativeDataModel();
            obj.ReleaseNoteQualitativeId = id;
			var list = Framework.Components.ReleaseLog.ReleaseNoteQualitativeDataManager.GetEntityDetails(obj, SessionVariables.RequestProfile);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            return list[0];
        }

        //
        // GET: /ReleaseNoteQualitative/
        public ActionResult Index(string searchString, int? page, int? pageSize)
        {
            var obj = new Framework.Components.ReleaseLog.DomainModel.ReleaseNoteQualitativeDataModel();
            obj.Name = searchString;
			var list = Framework.Components.ReleaseLog.ReleaseNoteQualitativeDataManager.GetEntitySearch(obj, SessionVariables.RequestProfile);

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
        // GET: /ReleaseNoteQualitative/Details/5

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
        // GET: /ReleaseNoteQualitative/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReleaseNoteQualitative/Create

        [HttpPost]
        public ActionResult Create(Framework.Components.ReleaseLog.DomainModel.ReleaseNoteQualitativeDataModel obj)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ReleaseLog.ReleaseNoteQualitativeDataManager.Create(obj, SessionVariables.RequestProfile);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //
        // GET: /ReleaseNoteQualitative/Edit/5

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
        // POST: /ReleaseNoteQualitative/Edit

        [HttpPost]
        public ActionResult Edit(Framework.Components.ReleaseLog.DomainModel.ReleaseNoteQualitativeDataModel obj)
        {
            if (ModelState.IsValid)
            {
				Framework.Components.ReleaseLog.ReleaseNoteQualitativeDataManager.Update(obj, SessionVariables.RequestProfile);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //
        // GET: /ReleaseNoteQualitative/Delete/5

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
        // POST: /ReleaseNoteQualitative/Delete

        [HttpPost]
        public ActionResult Delete(Framework.Components.ReleaseLog.DomainModel.ReleaseNoteQualitativeDataModel obj)
        {
            if (ModelState.IsValid)
            {
				Framework.Components.ReleaseLog.ReleaseNoteQualitativeDataManager.Delete(obj, SessionVariables.RequestProfile);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

    }
}
