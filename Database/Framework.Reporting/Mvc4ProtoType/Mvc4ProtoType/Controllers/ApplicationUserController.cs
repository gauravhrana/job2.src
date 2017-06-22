using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;

namespace Mvc4ProtoType.Controllers
{
    public class ApplicationUserController : Controller
    {


        //
        // GET: /Application/

        public ActionResult Index(string application, string applicationUserTitle, string searchString, int? page)
        {
            var data = new Framework.Components.ApplicationUser.ApplicationUser.Data();
            if (!string.IsNullOrEmpty(application))
            {
                data.ApplicationId = Convert.ToInt32(application);
            }
            if (!string.IsNullOrEmpty(applicationUserTitle))
            {
                data.ApplicationUserTitleId = Convert.ToInt32(applicationUserTitle);
            }
            
            var applicationList = Framework.Components.ApplicationUser.Application.GetList(10);
            var applicationUserTitleList = Framework.Components.ApplicationUser.ApplicationUserTitle.GetList(10);

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

            var applicationUserTitleListItems = new List<SelectListItem>();
            foreach (DataRow dr in applicationUserTitleList.Rows)
            {
                applicationUserTitleListItems.Add(new SelectListItem()
                {
                    Value = Convert.ToString(dr[Framework.Components.ApplicationUser.ApplicationUserTitle.DataColumns.ApplicationUserTitleId]),
                    Text = Convert.ToString(dr[Framework.Components.ApplicationUser.ApplicationUserTitle.DataColumns.Name])
                });
            }
            ViewBag.applicationUserTitle = new SelectList(applicationUserTitleListItems, "Value", "Text"); 

            data.ApplicationUserName = searchString;
            var dt = Framework.Components.ApplicationUser.ApplicationUser.Search(data, 10);

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page - 1 ?? 0);
            ViewBag.searchString = searchString;
            ViewBag.CurrentPage = pageNumber + 1;
            ViewBag.TotalPages = dt.Rows.Count / pageSize;
            ViewBag.applicationSearchString = application;
            ViewBag.applicationUserTitleSearchString = applicationUserTitle;
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
            var data = new Framework.Components.ApplicationUser.ApplicationUser.Data();
            data.ApplicationUserId = id;
            var dt = Framework.Components.ApplicationUser.ApplicationUser.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }
            data.ApplicationId          = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationId]);
            data.ApplicationUserTitleId = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationUserTitleId]);
            data.ApplicationUserName    = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationUserName]);
            data.FirstName              = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.FirstName]);
            data.MiddleName             = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.MiddleName]);
            data.LastName               = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.LastName]);
            data.Application            = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.Application]);
            data.ApplicationUserTitle   = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationUserTitle]);
            return View(data);
        }

        //
        // GET: /Application/Create

        public ActionResult Create()
        {
            var applicationList = Framework.Components.ApplicationUser.Application.GetList(10);
            var applicationUserTitleList = Framework.Components.ApplicationUser.ApplicationUserTitle.GetList(10);
            
            var applicationListItems = new List<SelectListItem>();
            foreach (DataRow dr in applicationList.Rows)
            {
                applicationListItems.Add(new SelectListItem()
                {
                    Value = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.ApplicationId]),
                    Text  = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.Name])
                });
            }
            ViewBag.applicationList = new SelectList(applicationListItems, "Value", "Text");
            
            var applicationUserTitleListItems = new List<SelectListItem>();
            foreach (DataRow dr in applicationUserTitleList.Rows)
            {
                applicationUserTitleListItems.Add(new SelectListItem()
                {
                    Value = Convert.ToString(dr[Framework.Components.ApplicationUser.ApplicationUserTitle.DataColumns.ApplicationUserTitleId]),
                    Text  = Convert.ToString(dr[Framework.Components.ApplicationUser.ApplicationUserTitle.DataColumns.Name])
                });
            }
            ViewBag.applicationUserTitleList = new SelectList(applicationUserTitleListItems, "Value", "Text"); 
            
            return View();
        }

        //
        // POST: /Application/Create

        [HttpPost]
        public ActionResult Create(Framework.Components.ApplicationUser.ApplicationUser.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.ApplicationUser.Create(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        //
        // GET: /Application/Edit/5

        public ActionResult Edit(int id)
        {

            var data = new Framework.Components.ApplicationUser.ApplicationUser.Data();
            data.ApplicationUserId = id;
            var dt = Framework.Components.ApplicationUser.ApplicationUser.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }
            data.ApplicationId          = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationId]);
            data.ApplicationUserTitleId = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationUserTitleId]);
            data.ApplicationUserName    = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationUserName]);
            data.FirstName              = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.FirstName]);
            data.MiddleName             = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.MiddleName]);
            data.LastName               = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.LastName]);
            data.Application            = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.Application]);
            data.ApplicationUserTitle   = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.Application]);

            var applicationList = Framework.Components.ApplicationUser.Application.GetList(10);
            var applicationUserTitleList = Framework.Components.ApplicationUser.ApplicationUserTitle.GetList(10);

            var applicationListItems = new List<SelectListItem>();
            foreach (DataRow dr in applicationList.Rows)
            {
                applicationListItems.Add(new SelectListItem()
                {
                    Value    = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.ApplicationId]),
                    Text     = Convert.ToString(dr[Framework.Components.ApplicationUser.Application.DataColumns.Name]),
                    Selected = (Convert.ToInt32(dr[Framework.Components.ApplicationUser.Application.DataColumns.ApplicationId]) == data.ApplicationId)
                });
            }
            ViewBag.applicationList = new SelectList(applicationListItems, "Value", "Text");

            var applicationUserTitleListItems = new List<SelectListItem>();
            foreach (DataRow dr in applicationUserTitleList.Rows)
            {
                applicationUserTitleListItems.Add(new SelectListItem()
                {
                    Value    = Convert.ToString(dr[Framework.Components.ApplicationUser.ApplicationUserTitle.DataColumns.ApplicationUserTitleId]),
                    Text     = Convert.ToString(dr[Framework.Components.ApplicationUser.ApplicationUserTitle.DataColumns.Name]),
                    Selected = (Convert.ToInt32(dr[Framework.Components.ApplicationUser.ApplicationUserTitle.DataColumns.ApplicationUserTitleId]) == data.ApplicationId)
                });
            }
            ViewBag.applicationUserTitleList = new SelectList(applicationUserTitleListItems, "Value", "Text"); 
            return View(data);
        }

        //
        // POST: /Application/Edit/5

        [HttpPost]
        public ActionResult Edit(Framework.Components.ApplicationUser.ApplicationUser.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.ApplicationUser.Update(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        //
        // GET: /Application/Delete/5

        public ActionResult Delete(int id)
        {
            var data = new Framework.Components.ApplicationUser.ApplicationUser.Data();
            data.ApplicationUserId = id;
            var dt = Framework.Components.ApplicationUser.ApplicationUser.GetDetails(data, 10);
            if (dt == null || dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }
            data.ApplicationId          = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationId]);
            data.ApplicationUserTitleId = Convert.ToInt32(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationUserTitleId]);
            data.ApplicationUserName    = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationUserName]);
            data.FirstName              = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.FirstName]);
            data.MiddleName             = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.MiddleName]);
            data.LastName               = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.LastName]);
            data.Application            = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.Application]);
            data.ApplicationUserTitle   = Convert.ToString(dt.Rows[0][Framework.Components.ApplicationUser.ApplicationUser.DataColumns.Application]);
            return View(data);
        }

        //
        // POST: /Application/Delete/5

        [HttpPost]
        public ActionResult Delete(Framework.Components.ApplicationUser.ApplicationUser.Data data)
        {
            if (ModelState.IsValid)
            {
                Framework.Components.ApplicationUser.ApplicationUser.Delete(data, 10);
                return RedirectToAction("Index");
            }

            return View(data);
        }

        public ActionResult SearchIndex(string searchString)
        {
            var data = new Framework.Components.ApplicationUser.ApplicationUser.Data();
            data.ApplicationUserName = searchString;
            var dt = Framework.Components.ApplicationUser.ApplicationUser.Search(data, 10);
            return View(dt);
        }

    }
}
