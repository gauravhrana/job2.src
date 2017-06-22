using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using PagedList;
using System.Configuration;

namespace Mvc4ProtoType.Controllers
{
    public class ApplicationController : Controller
    {

        //
        // GET: /Application/

        public ActionResult Index(string searchString, int? page)
        {
            var data = new Framework.Components.ApplicationUser.Application.Data();
            data.Name = searchString;
            var dt = Framework.Components.ApplicationUser.Application.Search(data, 10);
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page - 1 ?? 0);
            ViewBag.searchString = searchString;
            ViewBag.CurrentPage = pageNumber + 1;
            ViewBag.TotalPages = dt.Rows.Count / pageSize;
            if (dt.Rows.Count % pageSize != 0)
            {
                ViewBag.TotalPages = (dt.Rows.Count / pageSize) + 1;
            }
            dt = dt.AsEnumerable().Skip(pageNumber * pageSize).Take(pageSize).CopyToDataTable();
            return View(dt);
        }

        //
        // GET: /Application/Details/5

        public ActionResult Details(int id)
        {
            var data = new Framework.Components.ApplicationUser.Application.Data();
            data.ApplicationId = id;
            var dt = Framework.Components.ApplicationUser.Application.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }
            return View(dt.Rows[0]);
        }

        //
        // GET: /Application/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Application/Create

        [HttpPost]
        public ActionResult Create(Framework.Components.ApplicationUser.Application.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.Application.Create(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        //
        // GET: /Application/Edit/5

        public ActionResult Edit(int id)
        {
            var data = new Framework.Components.ApplicationUser.Application.Data();
            data.ApplicationId = id;
            var dt = Framework.Components.ApplicationUser.Application.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }

            data.Name = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.Application.DataColumns.Name]);
            data.Description = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.Application.DataColumns.Description]);
            data.SortOrder = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.Application.DataColumns.SortOrder]);
            return View(data);
        }

        //
        // POST: /Application/Edit/5

        [HttpPost]
        public ActionResult Edit(Framework.Components.ApplicationUser.Application.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.Application.Update(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        //
        // GET: /Application/Delete/5

        public ActionResult Delete(int id)
        {
            var data = new Framework.Components.ApplicationUser.Application.Data();
            data.ApplicationId = id;
            var dt = Framework.Components.ApplicationUser.Application.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }

            data.Name = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.Application.DataColumns.Name]);
            data.Description = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.Application.DataColumns.Description]);
            data.SortOrder = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.Application.DataColumns.SortOrder]);
            return View(data);
        }

        //
        // POST: /Application/Delete/5

        [HttpPost]
        public ActionResult Delete(Framework.Components.ApplicationUser.Application.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.Application.Delete(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        public ActionResult SearchIndex(string searchString, string description)
        {
            var data = new Framework.Components.ApplicationUser.Application.Data();
            data.Name = searchString;
            data.Description = description;
            var dt = Framework.Components.ApplicationUser.Application.Search(data, 10);
            return View(dt);
        }

    }
}
