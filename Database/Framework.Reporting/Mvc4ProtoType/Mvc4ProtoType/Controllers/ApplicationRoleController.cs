﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;

namespace Mvc4ProtoType.Controllers
{
    public class ApplicationRoleController : Controller
    {
        //
        // GET: /Application/

        public ActionResult Index(string application, string searchString, int? page)
        {
            var data = new Framework.Components.ApplicationUser.ApplicationRole.Data();
            data.Name = searchString;
            if (!string.IsNullOrEmpty(application))
            {
                data.ApplicationId = Convert.ToInt32(application);
            }
            var applicationList = Framework.Components.ApplicationUser.Application.GetList(10);
            var listItems = new List<SelectListItem>();
            foreach (DataRow dr in applicationList.Rows)
            {
                listItems.Add(new SelectListItem()
                {
                    Value = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.ApplicationId]),
                    Text = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.Name])
                });
            }
            ViewBag.application = new SelectList(listItems, "Value", "Text");
            var dt = Framework.Components.ApplicationUser.ApplicationRole.Search(data, 10);
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page - 1 ?? 0);
            ViewBag.searchString = searchString;
            ViewBag.CurrentPage = pageNumber + 1;
            ViewBag.TotalPages = dt.Rows.Count / pageSize;
            ViewBag.applicationSearchString = application;
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
            var data = new Framework.Components.ApplicationUser.ApplicationRole.Data();
            data.ApplicationRoleId = id;
            var dt = Framework.Components.ApplicationUser.ApplicationRole.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }
            data.Application = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.Application]);
            data.Name = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.Name]);
            data.Description = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.Description]);
            data.SortOrder = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.SortOrder]);
            return View(data);
        }

        //
        // GET: /Application/Create

        public ActionResult Create()
        {
            var applicationList = Framework.Components.ApplicationUser.Application.GetList(10);
            var listItems = new List<SelectListItem>();
            foreach(DataRow dr in applicationList.Rows)
            {
                listItems.Add(new SelectListItem()
                {
                    Value = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.ApplicationId]),
                    Text = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.Name])
                });
            }
            ViewBag.applicationList = new SelectList(listItems, "Value", "Text"); 
            return View();
        }

        //
        // POST: /Application/Create

        [HttpPost]
        public ActionResult Create(Framework.Components.ApplicationUser.ApplicationRole.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.ApplicationRole.Create(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        //
        // GET: /Application/Edit/5

        public ActionResult Edit(int id)
        {
            var data = new Framework.Components.ApplicationUser.ApplicationRole.Data();
            data.ApplicationRoleId = id;
            var dt = Framework.Components.ApplicationUser.ApplicationRole.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }
            data.ApplicationId = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.ApplicationId]);
            data.Name = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.Name]);
            data.Description = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.Description]);
            data.SortOrder = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.SortOrder]);

            var applicationList = Framework.Components.ApplicationUser.Application.GetList(10);
            var listItems = new List<SelectListItem>();
            foreach (DataRow dr in applicationList.Rows)
            {
                listItems.Add(new SelectListItem()
                {                    
                    Value = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.ApplicationId]),
                    Text = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.Name]),
                    Selected = (Convert.ToInt32(dr[Framework.Components.ApplicationUser.Application.DataColumns.ApplicationId]) == data.ApplicationId)
                });
            }
            ViewBag.applicationList = new SelectList(listItems, "Value", "Text");

            return View(data);
        }

        //
        // POST: /Application/Edit/5

        [HttpPost]
        public ActionResult Edit(Framework.Components.ApplicationUser.ApplicationRole.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.ApplicationRole.Update(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        //
        // GET: /Application/Delete/5

        public ActionResult Delete(int id)
        {
            var data = new Framework.Components.ApplicationUser.ApplicationRole.Data();
            data.ApplicationRoleId = id;
            var dt = Framework.Components.ApplicationUser.ApplicationRole.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }
            data.Application = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.Application]);
            data.Name = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.Name]);
            data.Description = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.Description]);
            data.SortOrder = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationRole.DataColumns.SortOrder]);
            return View(data);
        }

        //
        // POST: /Application/Delete/5

        [HttpPost]
        public ActionResult Delete(Framework.Components.ApplicationUser.ApplicationRole.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.ApplicationRole.Delete(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        public ActionResult SearchIndex(string searchString, string description)
        {
            var data = new Framework.Components.ApplicationUser.ApplicationRole.Data();
            data.Name = searchString;
            data.Description = description;
            var dt = Framework.Components.ApplicationUser.ApplicationRole.Search(data, 10);
            return View(dt);
        }
    }
}