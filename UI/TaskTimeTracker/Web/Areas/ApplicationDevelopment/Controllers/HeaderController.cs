﻿using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.ApplicationDevelopment.Controllers
{
    public class HeaderController : Controller
    {
        //
        // GET: /ApplicationDevelopment/Header/
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var applicationInfo = ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId];
            var applicationName = applicationInfo.Description;
            ViewBag.ApplicationName = applicationName;
            return PartialView();
        }

	}
}